using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Appointment.Application.Appointments.Commands.UpdateAppointment;
using Appointment.Application.Common.Models;
using Appointment.Application.Doctors.Commands;
using Appointment.Application.Doctors.Queries.GetDoctors;
using Appointment.Application.Salary.Queries;
using Appointment.Contracts.Appointments.Requests;
using Appointment.Contracts.Appointments.Responses;
using Appointment.Contracts.Common;
using Appointment.Contracts.Doctors;
using Appointment.Contracts.Schedules;
using Appointment.Domain.Core.Appointments;
using Appointment.Domain.Core.Doctors;
using Appointment.Domain.Core.Doctors.ValueObjects;
using Appointment.Domain.Core.Salary.ValueObjects;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.GenericCore.Extensions;
using Appointment.Presentation.Common.Mappings.Abstracts;
using WeeklySchedule = Appointment.Contracts.Schedules.WeeklySchedule;

namespace Appointment.Presentation.Common.Mappings
{
    public class DtoConvertor : IDtoConvertor
    {
        public DefineDoctorCommand ToCommand(DefineDoctorRequest request)
        {
            return new DefineDoctorCommand(DoctorNameValueObject.New(request.Name), DoctorFamilyValueObject.New(request.Family), ToSpeciality(request.Speciality));
        }

        public CreateDoctorScheduleCommand ToCommand(Guid doctorIdGuid, CreateDoctorScheduleRequest request)
        {
            var doctorId = DoctorIdValueObject.New(doctorIdGuid);
            return new CreateDoctorScheduleCommand(ToSchedule(doctorId, request.Schedule));
        }

        public GetDoctorsQuery ToQuery(GetDoctorsQueryParameters query)
        {
            return new GetDoctorsQuery(query.Name, query.Family, ToSpeciality(query.Speciality), query.PageSize, query.PageNumber);
        }

        public PagedResponse<DoctorResponse>? ToDto(Paged<DoctorAggregateRoot>? doctors)
        {
            if (doctors is null) 
                return new PagedResponse<DoctorResponse>(1, 1, 0, 0, 
                false, false, new List<DoctorResponse>());
            
            var doctorResponses = doctors.Data.Select(doctor => 
                new DoctorResponse(
                    doctor.DoctorId.Value, doctor.Name.Value, doctor.Family.Value, ToDto(doctor.Speciality), 
                    ToSchedule(doctor.Schedule)))
                .ToList(); 
            return new PagedResponse<DoctorResponse>(1, 2, doctorResponses.Count, 
                doctorResponses.Count, true, false, doctorResponses);
        }

        public PagedResponse<AppointmentListResponse>? ToDto(Paged<AppointmentAggregateRoot> appointments)
        {
            if (appointments is null) 
                return new PagedResponse<AppointmentListResponse>(1, 1, 0, 0, 
                    false, false, new List<AppointmentListResponse>());
            
            var appointmentListResponses = appointments.Data.Select(appointment => 
                    new AppointmentListResponse(
                        appointment.DoctorId.Value, 
                        appointment.PatientId.Value, 
                        appointment.AppointmentId.Value, 
                        appointment.AppointmentTime, 
                        appointment.AppointmentTime.AddMinutes(appointment.AppointmentDuration.Minutes), 
                        appointment.AppointmentDuration.Minutes))
                .ToList(); 
            return new PagedResponse<AppointmentListResponse>(1, 2, 
                appointmentListResponses.Count, 
                appointmentListResponses.Count, true, false, appointmentListResponses);
        }

        public UpdateAppointmentCommand ToCommand(string trackingCode, UpdateAppointmentRequest request)
        {
            return new UpdateAppointmentCommand(trackingCode, request.AppointmentStartDateTime, request.DurationMinutes);
        }

        public SalaryResponse? ToDto(SalaryValueObject aggr)
        {
            return new SalaryResponse(aggr.TotalAppointmentPerMinutes, aggr.Salary);
        }

        public GetSalaryQuery ToQuery(Guid doctorId, GetDoctorSalaryQueryParameters parametrs)
        {
            return new GetSalaryQuery(doctorId, parametrs.Year, parametrs.Month);
        }

        private WeeklySchedule ToSchedule(Domain.Core.Schedules.WeeklySchedule schedule)
        {
            return new WeeklySchedule(ToSchedule(schedule.DailySchedules));
        }

        private ImmutableArray<DailySchedule> ToSchedule(ImmutableArray<Schedule> schedules)
        {
            var list = schedules.Select(schedule => 
                    new DailySchedule(schedule.DayOfWeek.ConsiderSaturdayIsFirstDayOfWeek(), ToRange(schedule.DaySchedules)))
                .ToList();
            return list.ToImmutableArray();
        }

        private ImmutableArray<Contracts.Schedules.Range<DateTime>> ToRange(ImmutableArray<Domain.Core.Schedules.Range<DateTime>> ranges)
        {
            var list = ranges.Select(x => 
                new Contracts.Schedules.Range<DateTime>(x.Start, x.End))
                .ToList();
            return list.ToImmutableArray();
        }

        private DoctorSpeciality? ToDto(Domain.SubDomains.Doctors.DoctorSpeciality? dto) => dto switch
        {
            null => null,
            Domain.SubDomains.Doctors.DoctorSpeciality.General => DoctorSpeciality.GeneralPractitioner,
            Domain.SubDomains.Doctors.DoctorSpeciality.Specialist => DoctorSpeciality.Specialist,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), dto, null)
        };

        private Domain.Core.Schedules.WeeklySchedule ToSchedule(DoctorIdValueObject doctorId, WeeklySchedule request)
        {
            var dailySchedules = new List<Schedule>();
            foreach (var dailySchedule in request.DailySchedules)
            {
                var dailyRanges = dailySchedule.DaySchedules.Select(range => 
                    new Domain.Core.Schedules.Range<DateTime>(range.Start, range.End)).ToImmutableArray();
                dailySchedules.Add(new Schedule(doctorId, (DayOfWeek)dailySchedule.DayOfWeek, dailyRanges));
            }
            return new Domain.Core.Schedules.WeeklySchedule(dailySchedules.ToImmutableArray());
        }

        private Domain.SubDomains.Doctors.DoctorSpeciality? ToSpeciality(DoctorSpeciality? request) => request switch
        {
            null => null,
            DoctorSpeciality.GeneralPractitioner => Domain.SubDomains.Doctors.DoctorSpeciality.General,
            DoctorSpeciality.Specialist => Domain.SubDomains.Doctors.DoctorSpeciality.Specialist,
            DoctorSpeciality.SubSpecialty => Domain.SubDomains.Doctors.DoctorSpeciality.SubSpecialty,
            _ => throw new ArgumentOutOfRangeException(nameof(request), request, null)
        };
    }
}