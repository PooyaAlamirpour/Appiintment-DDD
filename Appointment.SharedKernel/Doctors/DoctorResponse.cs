using System;
using Appointment.Contracts.Schedules;

namespace Appointment.Contracts.Doctors
{
    public record DoctorResponse(Guid Id, string Name, string Family, DoctorSpeciality? Speciality, WeeklySchedule Schedule);
}