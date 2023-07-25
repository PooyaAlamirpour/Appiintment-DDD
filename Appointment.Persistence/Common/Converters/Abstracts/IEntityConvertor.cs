using System.Collections.Generic;
using System.Collections.Immutable;
using Appointment.Application.Common.Models;
using Appointment.Domain.Core.Appointments;
using Appointment.Domain.Core.Appointments.ValueObjects;
using Appointment.Domain.Core.Doctors;
using Appointment.Domain.Core.Schedules;
using Appointment.Persistence.Entities;

namespace Appointment.Persistence.Common.Converters.Abstracts
{
    public interface IEntityConvertor
    {
        DoctorEntity ToEntity(DoctorAggregateRoot doctor);
        ScheduleEntity ToEntity(WeeklySchedule request);
        List<ScheduleEntity> ToEntity(ImmutableArray<Schedule> schedules);
        Paged<DoctorAggregateRoot> ToDomain(Paged<DoctorEntity> entity);
        DoctorAggregateRoot? ToDomainModel(DoctorEntity? entity);
        DoctorValueObject? ToDomain(DoctorEntity? entity);
        AppointmentAggregateRoot? ToDomainModel(AppointmentEntity? entity);
        AppointmentEntity ToEntity(AppointmentAggregateRoot appointment);
        Paged<AppointmentAggregateRoot> ToDomain(Paged<AppointmentEntity> pagedListAsync);
    }
}