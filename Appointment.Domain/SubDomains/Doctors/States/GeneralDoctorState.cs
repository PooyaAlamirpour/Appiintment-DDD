using System;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.SubDomains.Doctors.States.Abstracts;

namespace Appointment.Domain.SubDomains.Doctors.States
{
    public record GeneralDoctorState() : IDoctorState
    {
        public Range<TimeSpan> DurationConstraint => new(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15));

        public int NumberOfAllowedOverlappingAppointment => 2;
        public decimal SalaryPerMinutes => 50000;
    }
}