using System;

namespace Charisma.Contracts.Appointments.Requests
{
    public record CreateEarliestAppointmentRequest(string DoctorId, string PatientId, int DurationMinutes);
}