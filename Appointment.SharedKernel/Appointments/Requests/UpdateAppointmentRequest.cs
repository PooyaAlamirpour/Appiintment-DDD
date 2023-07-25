using System;

namespace Appointment.Contracts.Appointments.Requests
{
    public record UpdateAppointmentRequest(DateTime AppointmentStartDateTime, int DurationMinutes);
}