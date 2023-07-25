using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Appointments;
using Charisma.Domain.Core.Appointments.ValueObjects;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Doctors.ValueObjects;
using Charisma.Domain.Core.Patients.ValueObjects;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.GenericCore.Extensions;
using Charisma.Domain.SubDomains.Doctors;
using Charisma.Persistence.Common.Converters.Abstracts;
using Charisma.Persistence.Entities;

namespace Charisma.Persistence.Common.Converters
{
    public class EntityConvertor : IEntityConvertor
    {
        public DoctorEntity ToEntity(DoctorAggregateRoot doctor)
        {
            return new DoctorEntity()
            {
                Family = doctor.Family.Value,
                Name = doctor.Name.Value,
                Speciality = doctor.Speciality
            };
        }

        public ScheduleEntity ToEntity(WeeklySchedule request)
        {
            throw new NotImplementedException();
        }

        public List<ScheduleEntity> ToEntity(ImmutableArray<Schedule> schedules)
        {
            List<ScheduleEntity> list = new();
            foreach (var dailySchedule in schedules)
            {
                list.AddRange(dailySchedule.DaySchedules
                    .Select(range => new ScheduleEntity()
                    {
                        Start = range.Start, 
                        End = range.End, 
                        DayOfWeek = dailySchedule.DayOfWeek.ConsiderSaturdayIsFirstDayOfWeek(),
                        DoctorId = dailySchedule.DoctorId.Value
                    }));
            }

            return list;
        }

        public Paged<DoctorAggregateRoot> ToDomain(Paged<DoctorEntity> entity)
        {
            List<DoctorAggregateRoot> doctors = entity.Data.Select(doctor =>
                {
                    var definedDoctor = DoctorAggregateRoot.Define(
                        DoctorNameValueObject.New(doctor.Name),
                        DoctorFamilyValueObject.New(doctor.Family),
                        DoctorSpeciality.Specialist);
                    
                    definedDoctor.SetId(doctor.Id);
                    definedDoctor.SetSchedule(ToDomain(doctor.Schedules));
                    return definedDoctor;
                })
                .ToList();
            
            return new Paged<DoctorAggregateRoot>(doctors, doctors.Count, entity.CurrentPage, entity.PageSize);
        }

        public DoctorAggregateRoot? ToDomainModel(DoctorEntity? entity)
        {
            if (entity is null) return null;
            var doctorName = DoctorNameValueObject.New(entity.Name);
            var doctorFamily = DoctorFamilyValueObject.New(entity.Family);
            return DoctorAggregateRoot.Define(doctorName, doctorFamily, entity.Speciality)
                .SetSchedule(ToDomain(entity.Schedules));
        }

        public DoctorValueObject? ToDomain(DoctorEntity? entity)
        {
            if (entity is null) return null;
            return DoctorValueObject.New(entity.Speciality, ToDomain(entity.Schedules));
        }

        public AppointmentAggregateRoot? ToDomainModel(AppointmentEntity? entity)
        {
            if (entity is null) return null;
            var doctor = DoctorValueObject.New(entity.Doctor.Speciality, ToDomain(entity.Doctor.Schedules));
            return AppointmentAggregateRoot.Define(entity.DoctorId,  entity.PatientId, doctor, entity.AppointmentDateTimeStart, entity.DurationMinutes);
        }

        public AppointmentEntity ToEntity(AppointmentAggregateRoot appointment)
        {
            return new AppointmentEntity()
            {
                AppointmentId = appointment.AppointmentId.Value,
                PatientId = appointment.PatientId.Value,
                DoctorId = appointment.DoctorId.Value,
                DurationMinutes = appointment.AppointmentDuration.Minutes,
                CreatedDateTime = DateTime.Now,
                AppointmentDateTimeStart = appointment.AppointmentTime,
                AppointmentDateTimeEnd = appointment.AppointmentTime.AddMinutes(appointment.AppointmentDuration.Minutes)
            };
        }

        public Paged<AppointmentAggregateRoot> ToDomain(Paged<AppointmentEntity> entity)
        {
            List<AppointmentAggregateRoot> appointments = entity.Data.Select(appointmentEntity =>
                {
                    var appointment = AppointmentAggregateRoot.Define(
                        appointmentEntity.DoctorId,
                        appointmentEntity.PatientId,
                        DoctorValueObject.New(appointmentEntity.Doctor.Speciality, ToDomain(appointmentEntity.Doctor.Schedules)), 
                        appointmentEntity.AppointmentDateTimeStart, appointmentEntity.DurationMinutes);
                    appointment.SetId(appointmentEntity.AppointmentId);
                    return appointment;
                })
                .ToList();
            
            return new Paged<AppointmentAggregateRoot>(appointments, appointments.Count, entity.CurrentPage, entity.PageSize);
        }

        private WeeklySchedule ToDomain(ICollection<ScheduleEntity> doctorSchedules)
        {
            List<Schedule> dailySchedules = doctorSchedules.Select(entity => 
                new Schedule(DoctorIdValueObject.New(entity.DoctorId),
                    ToDayOfWeek(entity.DayOfWeek),
                    new List<Range<DateTime>>()
                    {
                        new(entity.Start, entity.End)
                    }.ToImmutableArray()))
                .ToList();
            
            return new WeeklySchedule(dailySchedules.ToImmutableArray());
        }

        private DayOfWeek ToDayOfWeek(int dayOfWeek)
        {
            return dayOfWeek switch
            {
                0 => DayOfWeek.Saturday,
                1 => DayOfWeek.Sunday,
                2 => DayOfWeek.Monday,
                3 => DayOfWeek.Tuesday,
                4 => DayOfWeek.Wednesday,
                5 => DayOfWeek.Thursday,
                6 => DayOfWeek.Friday,
                _ => throw new ArgumentException("Invalid day of week")
            };
        }
    }
}