using System;

namespace Charisma.Contracts.Appointments.Responses
{
    public record CreateAppointmentResponse(Guid Id, string AppointmentId, string TrackingCode, DateTime CreatedOnUtc);
}