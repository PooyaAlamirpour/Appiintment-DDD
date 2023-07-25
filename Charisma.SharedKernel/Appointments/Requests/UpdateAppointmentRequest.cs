using System;

namespace Charisma.Contracts.Appointments.Requests
{
    public record UpdateAppointmentRequest(DateTime AppointmentStartDateTime, int DurationMinutes);
}