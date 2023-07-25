using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Charisma.Domain.Core.Appointments.ValueObjects;
using Charisma.Domain.Core.Doctors.ValueObjects;
using Charisma.Domain.Core.Patients.ValueObjects;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.GenericCore.Abstractions;
using Charisma.Domain.GenericCore.Exceptions;
using Charisma.Domain.GenericCore.Extensions;
using Charisma.Domain.GenericCore.Interfaces;
using Charisma.Domain.SubDomains;
using Charisma.Domain.SubDomains.Doctors.States;
using EX = Charisma.Domain.GenericCore.Exceptions.AppointmentExceptionCodes;

namespace Charisma.Domain.Core.Appointments
{
    public sealed class AppointmentAggregateRoot : AggregateRoot<AppointmentIdValueObject>, IAuditableEntity, ISoftDeletableEntity, IAppointmentAggregateRoot
    {
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ModifiedOnUtc { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }
        public bool IsDeleted { get; private set; }
        public DoctorValueObject Doctor { get; private set; }
        public DoctorIdValueObject DoctorId { get; private set; }
        public PatientIdValueObject PatientId { get; private set; }
        public TrackingCodeValueObject TrackingCode { get; private set; }
        public ImmutableArray<AppointmentHistoryValueObject>? PatientAppointmentHistories { get; private set; }
        public ImmutableArray<AppointmentHistoryValueObject>? DoctorAppointmentHistories { get; private set; }
        public DateTime AppointmentTime { get; private set; }
        public TimeSpan AppointmentDuration { get; private set; }
        public AppointmentIdValueObject AppointmentId { get; private set; }

        public AppointmentAggregateRoot() { }
        private AppointmentAggregateRoot(Guid doctorId, Guid patientId, DoctorValueObject doctor, TrackingCodeValueObject trackingCode, 
            DateTime startDateTime, int durationMinutes)
        {
            TrackingCode = trackingCode;
            AppointmentTime = startDateTime;
            AppointmentDuration = TimeSpan.FromMinutes(durationMinutes);
            Doctor = doctor;
            DoctorId = DoctorIdValueObject.New(doctorId);
            PatientId = PatientIdValueObject.New(patientId);
        }

        public static AppointmentAggregateRoot Define(Guid doctorId, Guid patientId, DoctorValueObject doctor,
            DateTime startDateTime, int durationMinutes)
        {
            var appointment = new AppointmentAggregateRoot(doctorId, patientId, doctor, TrackingCodeValueObject.New(), startDateTime, durationMinutes);
            return appointment;
        }
        
        public async Task<AppointmentIdValueObject> Do(AppointmentAggregateRoot arg)
        {
            AppointmentTimeIsOutOfTheClinicWorkingHours(arg.AppointmentTime);
            AppointmentDurationNotAppropriateToTheDoctorSpeciality(arg.Doctor, arg.AppointmentDuration);
            AppointmentTimeDuringDoctorNotPresents(arg.Doctor, arg.AppointmentTime);
            AppointmentsOfPatientHasNotOverlap(arg.PatientAppointmentHistories, arg.AppointmentTime, arg.AppointmentDuration);
            PatientMoreThanTwoAppointmentAtTheSameDate(arg.PatientAppointmentHistories);
            InvalidOverlappingAppointment(arg.DoctorAppointmentHistories, arg.Doctor, arg.AppointmentTime, arg.AppointmentDuration);

            return AppointmentIdValueObject.New(arg.AppointmentTime, arg.DoctorId);
        }

        private void AppointmentTimeIsOutOfTheClinicWorkingHours(DateTime appointmentTime)
        {
            var workingHours = new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(18, 0));
            var workingDays = new Range<int>((int)WorkingDay.Saturday, (int)WorkingDay.Wednesday);

            var requestedDay = appointmentTime.DayOfWeek.ConsiderSaturdayIsFirstDayOfWeek();
            if (workingHours.Contains(appointmentTime.ToTimeOnly()) &&
                workingDays.Contains(requestedDay)) return;
            
            throw new BusinessException(EX.MustBeWithinWorkingHourOfClinic.Code, 
                EX.MustBeWithinWorkingHourOfClinic.Message);
        }

        private void AppointmentDurationNotAppropriateToTheDoctorSpeciality(DoctorValueObject doctor, TimeSpan duration)
        {
            if (doctor.DoctorState.DurationConstraint.Contains(duration)) return;
            throw new BusinessException(EX.MustBeAppropriateToTheDoctorSpeciality.Code, 
                EX.MustBeAppropriateToTheDoctorSpeciality.Message);
        }

        private void AppointmentTimeDuringDoctorNotPresents(DoctorValueObject doctor, DateTime appointmentTime)
        {
            var weeklySchedule = doctor.WeeklySchedule;
            if (weeklySchedule is null) 
                throw new BusinessException(EX.TheRequestedDoctorDoesNotHaveTime.Code, 
                    EX.TheRequestedDoctorDoesNotHaveTime.Message);
            
            if (weeklySchedule.AcceptAppointmentTime(appointmentTime)) return;
            throw new BusinessException(EX.MustBeDuringTheDoctorsPresents.Code, 
                EX.MustBeDuringTheDoctorsPresents.Message);
        }

        private void PatientMoreThanTwoAppointmentAtTheSameDate(ImmutableArray<AppointmentHistoryValueObject>? patientsAppointments)
        {
            if (patientsAppointments is null) return;
            if (patientsAppointments.Value.Length < 3) return;
            throw new BusinessException(EX.PatientMustHaveLessThanTwoAppointmentAtTheSameDay.Code, 
                EX.PatientMustHaveLessThanTwoAppointmentAtTheSameDay.Message);
        }

        private void AppointmentsOfPatientHasNotOverlap(ImmutableArray<AppointmentHistoryValueObject>? patientsAppointments,
            DateTime appointmentTime, TimeSpan appointmentDuration)
        {
            if (!patientsAppointments.HasValue) return;
            
            if (patientsAppointments.Value.HasNotOverlapWith(appointmentTime, appointmentDuration)) return;
            throw new BusinessException(EX.AppointmentsOfPatientShouldNotOverlap.Code, 
                EX.AppointmentsOfPatientShouldNotOverlap.Message);
        }

        private void InvalidOverlappingAppointment(
            ImmutableArray<AppointmentHistoryValueObject>? doctorAppointmentHistoryValueObjects, DoctorValueObject doctor,
            DateTime appointmentTime, TimeSpan appointmentDuration)
        {
            if (doctorAppointmentHistoryValueObjects is null) return;
            if (doctorAppointmentHistoryValueObjects?.NumOfOverlapWith(appointmentTime, appointmentDuration) < 
                doctor.DoctorState.NumberOfAllowedOverlappingAppointment) return;
            
            throw new BusinessException(EX.TheNumOfDoctorsOverlappingMustNotMoreThanSpecificNum.Code, 
                EX.TheNumOfDoctorsOverlappingMustNotMoreThanSpecificNum.Message);
        }

        public AppointmentAggregateRoot SetPatientAppointmentHistory(List<AppointmentHistoryValueObject> patientAppointmentsInDay)
        {
            PatientAppointmentHistories = patientAppointmentsInDay.ToImmutableArray();
            return this;
        }
        
        public AppointmentAggregateRoot SetDoctorAppointmentHistory(List<AppointmentHistoryValueObject> doctorAppointmentsInDay)
        {
            DoctorAppointmentHistories = doctorAppointmentsInDay.ToImmutableArray();
            return this;
        }

        public void SetId(AppointmentIdValueObject appointmentId)
        {
            AppointmentId = appointmentId;
        }

        public void SetId(string appointmentId)
        {
            AppointmentId = AppointmentIdValueObject.New(appointmentId);
        }
    }
}