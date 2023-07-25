using System;
using Charisma.Domain.Core.Schedules;

namespace Charisma.Domain.SubDomains.Doctors.States.Abstracts
{
    public interface IDoctorState
    {
        Range<TimeSpan> DurationConstraint { get; }
        int NumberOfAllowedOverlappingAppointment { get;  }
        public decimal SalaryPerMinutes { get; }
    }
}