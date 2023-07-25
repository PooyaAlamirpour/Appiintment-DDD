using System;
using Appointment.Contracts.Common;

namespace Appointment.Contracts.Appointments.Requests
{
    public class GetAppointmentsQueryParameters : PaginationQueryParameters
    {
        public Guid PatientId { get; set; } = Guid.Empty;
        public Guid DoctorId { get; set; } = Guid.Empty;
        public DateTime AppointmentStart { get; set; } = DateTime.MinValue;
        public DateTime AppointmentEnd { get; set; } = DateTime.MinValue;
        public string TrackingCode { get; set; } = string.Empty;
    }
}