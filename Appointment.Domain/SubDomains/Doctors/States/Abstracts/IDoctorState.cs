using System;
using Appointment.Domain.Core.Schedules;

namespace Appointment.Domain.SubDomains.Doctors.States.Abstracts
{
    public interface IDoctorState
    {
        Range<TimeSpan> DurationConstraint { get; }
        int NumberOfAllowedOverlappingAppointment { get;  }
        public decimal SalaryPerMinutes { get; }
    }
}