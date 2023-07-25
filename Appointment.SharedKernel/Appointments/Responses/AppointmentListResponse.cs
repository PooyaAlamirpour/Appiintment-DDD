using System;

namespace Appointment.Contracts.Appointments.Responses
{
    public record AppointmentListResponse(Guid DoctorId, Guid PatientId, string TrackingCode, 
        DateTime AppointmentStartTime, DateTime AppointmentEndTime, int DurationMinutes);
}