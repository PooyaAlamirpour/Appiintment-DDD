using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Appointments;
using Charisma.Domain.Core.Appointments.ValueObjects;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Schedules;
using Charisma.Persistence.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore.Query;

namespace Charisma.Persistence.Common.Converters.Abstracts
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