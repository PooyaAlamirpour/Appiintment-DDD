using System;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.SubDomains.Doctors.States.Abstracts;

namespace Appointment.Domain.SubDomains.Doctors.States
{
    public record SubSpecialtyDoctorState() : IDoctorState
    {
        public Range<TimeSpan> DurationConstraint => new(TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(60));
        public int NumberOfAllowedOverlappingAppointment => 4;
        public decimal SalaryPerMinutes => 200000;
    }
}