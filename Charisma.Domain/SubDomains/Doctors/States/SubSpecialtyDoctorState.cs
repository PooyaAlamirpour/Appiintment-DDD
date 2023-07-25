using System;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.SubDomains.Doctors.States.Abstracts;

namespace Charisma.Domain.SubDomains.Doctors.States
{
    public record SubSpecialtyDoctorState() : IDoctorState
    {
        public Range<TimeSpan> DurationConstraint => new(TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(60));
        public int NumberOfAllowedOverlappingAppointment => 4;
        public decimal SalaryPerMinutes => 200000;
    }
}