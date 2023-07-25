using System;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.SubDomains.Doctors.States.Abstracts;

namespace Appointment.Domain.SubDomains.Doctors.States
{
    public record SpecialistDoctorState() : IDoctorState
    {
        public Range<TimeSpan> DurationConstraint => new(TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(30));

        public int NumberOfAllowedOverlappingAppointment => 3;
        public decimal SalaryPerMinutes => 100000;
    }
}