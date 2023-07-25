namespace Appointment.Contracts.Appointments.Requests
{
    public record CreateEarliestAppointmentRequest(string DoctorId, string PatientId, int DurationMinutes);
}