using System;
using Charisma.Contracts.Schedules;

namespace Charisma.Contracts.Doctors
{
    public record DoctorResponse(Guid Id, string Name, string Family, DoctorSpeciality? Speciality, WeeklySchedule Schedule);
}