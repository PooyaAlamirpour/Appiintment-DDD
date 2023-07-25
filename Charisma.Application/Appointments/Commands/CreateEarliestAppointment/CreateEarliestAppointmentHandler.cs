using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Messages;
using Charisma.Domain.Core.Appointments;
using Charisma.Domain.Core.Appointments.ValueObjects;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.GenericCore.Errors;
using Charisma.Domain.GenericCore.Exceptions;
using Charisma.Domain.GenericCore.Extensions;
using ErrorOr;
using EX = Charisma.Domain.GenericCore.Exceptions.AppointmentExceptionCodes;

namespace Charisma.Application.Appointments.Commands.CreateEarliestAppointment
{
    public class CreateEarliestAppointmentHandler : ICommandHandler<CreateEarliestAppointmentCommand, ErrorOr<AppointmentIdValueObject>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentAggregateRoot _appointmentAggregateRoot;

        public CreateEarliestAppointmentHandler(IUnitOfWork unitOfWork, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository, IAppointmentAggregateRoot appointmentAggregateRoot)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _appointmentAggregateRoot = appointmentAggregateRoot;
        }

        public async Task<ErrorOr<AppointmentIdValueObject>> Handle(CreateEarliestAppointmentCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor is null) return Errors.Doctor.NotFound(request.DoctorId.ToString());
            
            var doctorValueObject = DoctorValueObject.New(doctor.Speciality, doctor.Schedule);

            var doctorAppointmentRanges = await GetDoctorAppointmentsFromNow(request);

            var sortedDoctorFreeTimes = ExtractSortedDoctorFreeTime(request, doctor, doctorAppointmentRanges);

            var appointmentHistories = await GetPatientAppointmentsFromNow(request);

            AppointmentIdValueObject? appointmentId = null;
            AppointmentAggregateRoot? appointment = null;
            foreach (var schedule in sortedDoctorFreeTimes)
            {
                try
                {
                    var appointmentHistoriesInADay = PatientAppointmentsInASpecificDay(appointmentHistories, schedule);
                    
                    appointment = AppointmentAggregateRoot
                        .Define(request.DoctorId,  request.PatientId, doctorValueObject, schedule.Start, request.DurationMinutes)
                        .SetPatientAppointmentHistory(appointmentHistoriesInADay);
                    
                    appointmentId = await _appointmentAggregateRoot.Do(appointment);
                    appointment.SetId(appointmentId);
                    break;
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            if (string.IsNullOrWhiteSpace(appointmentId?.Value)) 
                throw new BusinessException(EX.ThereIsNotFreeTimeForRequestedDoctor.Code, 
                    EX.ThereIsNotFreeTimeForRequestedDoctor.Message);
            
            try
            {
                await _appointmentRepository.AddAsync(appointment, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Errors.Appointment.Exception(ex.Message);
            }

            return appointmentId;
        }

        private static List<AppointmentHistoryValueObject>? PatientAppointmentsInASpecificDay(List<AppointmentHistoryValueObject>? appointmentHistories, Range<DateTime> schedule)
        {
            var appointmentHistoriesInADay =
                appointmentHistories.Where(x => x.AppointmentTime.ToDateOnly() >= schedule.Start.ToDateOnly())
                    .Where(x => x.AppointmentTime.ToDateOnly() <= schedule.End.ToDateOnly())
                    .ToList();
            return appointmentHistoriesInADay;
        }

        private static ImmutableArray<Range<DateTime>> ExtractSortedDoctorFreeTime(CreateEarliestAppointmentCommand request,
            DoctorAggregateRoot? doctor, ImmutableArray<Range<DateTime>> doctorAppointmentRanges)
        {
            var doctorScheduleRanges = doctor.Schedule.DailySchedules
                .SelectMany(x => x.DaySchedules
                    .Select(s => new Range<DateTime>(s.Start, s.End)));

            var sortedDoctorScheduleRanges = doctorScheduleRanges.OrderBy(x => x.Start).ToImmutableArray();
            var splitRanges = sortedDoctorScheduleRanges.SplitChunk(request.DurationMinutes);
            var freeScheduleList = splitRanges.RemoveOverlapWith(doctorAppointmentRanges);
            return freeScheduleList;
        }

        private async Task<ImmutableArray<Range<DateTime>>> GetDoctorAppointmentsFromNow(CreateEarliestAppointmentCommand request)
        {
            var doctorAppointments = await _appointmentRepository.GetDoctorAppointmentsFromNow(request.DoctorId);
            var doctorAppointmentRanges = doctorAppointments.Select(x =>
                    new Range<DateTime>(x.AppointmentTime, x.AppointmentTime.AddMinutes(x.AppointmentDuration.Minutes)))
                .ToImmutableArray();
            return doctorAppointmentRanges;
        }

        private async Task<List<AppointmentHistoryValueObject>> GetPatientAppointmentsFromNow(CreateEarliestAppointmentCommand request)
        {
            var patientAppointmentsInDay =
                await _appointmentRepository.GetPatientAppointmentsFromNow(request.PatientId);
            var appointmentHistories = patientAppointmentsInDay.Select(x =>
                AppointmentHistoryValueObject.New(x.AppointmentTime, x.AppointmentDuration)).ToList();
            return appointmentHistories;
        }
    }
}