using System;

namespace Charisma.Contracts.Appointments.Responses
{
    public record AppointmentListResponse(Guid DoctorId, Guid PatientId, string TrackingCode, 
        DateTime AppointmentStartTime, DateTime AppointmentEndTime, int DurationMinutes);
}