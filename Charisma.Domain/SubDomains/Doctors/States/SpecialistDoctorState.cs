using System;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.SubDomains.Doctors.States.Abstracts;

namespace Charisma.Domain.SubDomains.Doctors.States
{
    public record SpecialistDoctorState() : IDoctorState
    {
        public Range<TimeSpan> DurationConstraint => new(TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(30));

        public int NumberOfAllowedOverlappingAppointment => 3;
        public decimal SalaryPerMinutes => 100000;
    }
}