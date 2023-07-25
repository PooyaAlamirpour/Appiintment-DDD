using System;
using Appointment.Application.Common.Messages;
using Appointment.Application.Common.Models;
using Appointment.Domain.Core.Appointments;
using ErrorOr;

namespace Appointment.Application.Appointments.Queries.GetAppointments
{
    public record GetAppointmentsQuery(
        Guid PatientId,
        Guid DoctorId,
        DateTime AppointmentStart,
        DateTime AppointmentEnd,
        string TrackingCode,
        int PageSize, int PageNumber) : IQuery<ErrorOr<Paged<AppointmentAggregateRoot>>>;
}