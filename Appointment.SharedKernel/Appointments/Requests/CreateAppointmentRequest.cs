using System;

namespace Appointment.Contracts.Appointments.Requests
{
    public record CreateAppointmentRequest(string DoctorId, string PatientId, int DurationMinutes, DateTime StartDateTime);
}