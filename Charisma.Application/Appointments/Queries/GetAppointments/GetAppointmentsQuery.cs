using System;
using Charisma.Application.Common.Messages;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Appointments;
using ErrorOr;

namespace Charisma.Application.Appointments.Queries.GetAppointments
{
    public record GetAppointmentsQuery(
        Guid PatientId,
        Guid DoctorId,
        DateTime AppointmentStart,
        DateTime AppointmentEnd,
        string TrackingCode,
        int PageSize, int PageNumber) : IQuery<ErrorOr<Paged<AppointmentAggregateRoot>>>;
}