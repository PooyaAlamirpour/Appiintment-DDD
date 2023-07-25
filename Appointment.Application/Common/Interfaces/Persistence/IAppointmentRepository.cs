using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Models;
using Appointment.Domain.Core.Appointments;

namespace Appointment.Application.Common.Interfaces.Persistence
{
    public interface IAppointmentRepository
    {
        Task<AppointmentAggregateRoot> GetByIdAsync(Guid id);
        Task<List<AppointmentAggregateRoot>> GetPatientAppointmentsInDay(Guid patientId, DateOnly appointmentDate);
        Task<List<AppointmentAggregateRoot>> GetPatientAppointmentsFromNow(Guid patientId);
        Task<List<AppointmentAggregateRoot>> GetDoctorAppointmentsFromNow(Guid doctorId);
        Task<List<AppointmentAggregateRoot>> GetDoctorAppointmentsForSpecificMonth(Guid doctorId, int year, int month);
        Task<Guid> AddAsync(AppointmentAggregateRoot appointment, CancellationToken cancellationToken);
        Task<Paged<AppointmentAggregateRoot>> FindManyWithPaginationAsync(Guid doctorId, Guid patientId, DateTime appointmentStart, 
            DateTime appointmentEnd, string trackingCode, int requestPageSize, int requestPageNumber, CancellationToken cancellationToken);

        Task<AppointmentAggregateRoot?> GetByTrackingCodeAsync(string trackingCode, CancellationToken cancellationToken);
        Task DeleteByTrackingCode(string trackingCode);
    }
}