using System;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.SubDomains.Doctors.States.Abstracts;

namespace Charisma.Domain.SubDomains.Doctors.States
{
    public record GeneralDoctorState() : IDoctorState
    {
        public Range<TimeSpan> DurationConstraint => new(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15));

        public int NumberOfAllowedOverlappingAppointment => 2;
        public decimal SalaryPerMinutes => 50000;
    }
}