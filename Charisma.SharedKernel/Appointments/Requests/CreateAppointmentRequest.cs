using System;

namespace Charisma.Contracts.Appointments.Requests
{
    public record CreateAppointmentRequest(string DoctorId, string PatientId, int DurationMinutes, DateTime StartDateTime);
}