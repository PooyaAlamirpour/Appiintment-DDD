using System;
using Charisma.Contracts.Schedules;

namespace Charisma.Contracts.Doctors
{
    public record CreateDoctorScheduleRequest(WeeklySchedule Schedule);
}