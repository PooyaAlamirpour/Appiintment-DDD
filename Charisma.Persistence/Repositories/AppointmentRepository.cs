using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Appointments;
using Charisma.Domain.GenericCore.Extensions;
using Charisma.Persistence.Common.Converters.Abstracts;
using Charisma.Persistence.Common.Extensions;
using Charisma.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Charisma.Persistence.Repositories
{
    internal sealed class AppointmentRepository : IAppointmentRepository
    {
        private readonly IEntityConvertor _convertor;
        private readonly IAsyncRepository<AppointmentEntity, Guid> _appointmentRepository;

        public AppointmentRepository(IEntityConvertor convertor, IAsyncRepository<AppointmentEntity, Guid> appointmentRepository)
        {
            _convertor = convertor;
            _appointmentRepository = appointmentRepository;
        }
        
        public async Task<AppointmentAggregateRoot> GetByIdAsync(Guid id)
        {
            var entity = await _appointmentRepository.FindByIdAsync(id);
            return _convertor.ToDomainModel(entity);
        }

        public async Task<List<AppointmentAggregateRoot>> GetPatientAppointmentsInDay(Guid patientId, DateOnly appointmentDate)
        {
            var appointments = await _appointmentRepository.GetWithFilterAsync(x => 
                x.PatientId == patientId && 
                x.AppointmentDateTimeStart.Year <= appointmentDate.Year &&
                x.AppointmentDateTimeStart.Month <= appointmentDate.Month &&
                x.AppointmentDateTimeStart.Day <= appointmentDate.Day &&
                x.AppointmentDateTimeEnd.Year >= appointmentDate.Year && 
                x.AppointmentDateTimeEnd.Month >= appointmentDate.Month && 
                x.AppointmentDateTimeEnd.Day >= appointmentDate.Day);
            
            var attachedPatients = appointments?.Include(x => x.Patient);
            var attachedSchedule = attachedPatients?.Include(x => x.Doctor.Schedules);
            
            return attachedSchedule?.Select(x => 
                    AppointmentAggregateRoot.Define(x.DoctorId,  patientId, _convertor.ToDomain(x.Doctor), 
                        x.AppointmentDateTimeStart, x.DurationMinutes))
                .ToList();
        }

        public async Task<List<AppointmentAggregateRoot>> GetPatientAppointmentsFromNow(Guid patientId)
        {
            var now = DateTime.Now;
            var appointments = await _appointmentRepository.GetWithFilterAsync(x => 
                x.PatientId == patientId && 
                x.AppointmentDateTimeStart.Year >= now.Year &&
                x.AppointmentDateTimeStart.Month >= now.Month &&
                x.AppointmentDateTimeStart.Day >= now.Day);
            
            var attachedPatients = appointments?.Include(x => x.Patient);
            var attachedSchedule = attachedPatients?.Include(x => x.Doctor.Schedules);
            
            return attachedSchedule?.Select(x => 
                    AppointmentAggregateRoot.Define(x.DoctorId,  x.PatientId, _convertor.ToDomain(x.Doctor), 
                        x.AppointmentDateTimeStart, x.DurationMinutes))
                .ToList();
        }

        public async Task<List<AppointmentAggregateRoot>> GetDoctorAppointmentsFromNow(Guid doctorId)
        {
            var now = DateTime.Now;
            var appointments = await _appointmentRepository.GetWithFilterAsync(x => 
                x.DoctorId == doctorId && 
                x.AppointmentDateTimeStart.Year >= now.Year &&
                x.AppointmentDateTimeStart.Month >= now.Month &&
                x.AppointmentDateTimeStart.Day >= now.Day);
            
            var attachedPatients = appointments?.Include(x => x.Patient);
            var attachedSchedule = attachedPatients?.Include(x => x.Doctor.Schedules);
            
            return attachedSchedule?.Select(x => 
                    AppointmentAggregateRoot.Define(x.DoctorId,  x.PatientId, _convertor.ToDomain(x.Doctor), 
                        x.AppointmentDateTimeStart, x.DurationMinutes))
                .ToList();
        }

        public async Task<List<AppointmentAggregateRoot>> GetDoctorAppointmentsForSpecificMonth(Guid doctorId, int year, int month)
        {
            var appointments = await _appointmentRepository.GetWithFilterAsync(x => 
                x.DoctorId == doctorId && 
                x.AppointmentDateTimeStart.Year == year &&
                x.AppointmentDateTimeStart.Month == month);
            
            var attachedPatients = appointments?.Include(x => x.Patient);
            var attachedSchedule = attachedPatients?.Include(x => x.Doctor.Schedules);
            
            return attachedSchedule?.Select(x => 
                    AppointmentAggregateRoot.Define(x.DoctorId,  x.PatientId, _convertor.ToDomain(x.Doctor), 
                        x.AppointmentDateTimeStart, x.DurationMinutes))
                .ToList();
        }

        public async Task<Guid> AddAsync(AppointmentAggregateRoot appointment, CancellationToken cancellationToken)
        {
            var appointmentEntity = _convertor.ToEntity(appointment);
            await _appointmentRepository.AddAsync(appointmentEntity, cancellationToken);
            return appointmentEntity.Id;
        }

        public async Task<Paged<AppointmentAggregateRoot>> FindManyWithPaginationAsync(Guid doctorId, Guid patientId, 
            DateTime appointmentStart, DateTime appointmentEnd, string trackingCode,
            int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var query = await _appointmentRepository.GetWithFilterAsync(null);
            if (doctorId != Guid.Empty)
            {
                query = query?.Where(x => x.DoctorId == doctorId);
            }
            if (patientId != Guid.Empty)
            {
                query = query?.Where(x => x.PatientId == patientId);
            }
            if (appointmentStart != DateTime.MinValue)
            {
                query = query?.Where(x => x.AppointmentDateTimeStart.ToDateOnly() == appointmentStart.ToDateOnly());
            }
            if (appointmentEnd != DateTime.MinValue)
            {
                query = query?.Where(x => x.AppointmentDateTimeEnd.ToDateOnly() == appointmentEnd.ToDateOnly());
            }
            if (trackingCode != string.Empty)
            {
                query = query?.Where(x => x.AppointmentId == trackingCode);
            }
            
            var attachedDoctor = query?.Include(x => x.Doctor);
            var attachedSchedule = attachedDoctor?.Include(x => x.Doctor.Schedules);
            var pagedListAsync = await attachedSchedule?.ToPagedListAsync(pageNumber, pageSize, cancellationToken)!;
            return _convertor.ToDomain(pagedListAsync);
        }

        public async Task<AppointmentAggregateRoot?> GetByTrackingCodeAsync(string trackingCode, CancellationToken cancellationToken)
        {
            var query = await _appointmentRepository.GetWithFilterAsync(x => x.AppointmentId == trackingCode);
            if (query is null) return null;
            var attachedDoctor = query?.Include(x => x.Doctor);
            var attachedSchedules = attachedDoctor?.Include(x => x.Doctor.Schedules)
                .SingleOrDefault();
            
            return _convertor.ToDomainModel(attachedSchedules);
        }

        public async Task DeleteByTrackingCode(string trackingCode)
        {
            var query = await _appointmentRepository.GetWithFilterAsync(x => x.AppointmentId == trackingCode);
            _appointmentRepository.Remove(query?.SingleOrDefault());
        }
    }
}