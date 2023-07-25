using System;
using System.Collections.Immutable;
using System.Linq;
using Charisma.Domain.GenericCore.Extensions;

namespace Charisma.Domain.Core.Schedules
{
    public record WeeklySchedule(ImmutableArray<Schedule> DailySchedules)
    {
        public bool AcceptAppointmentTime(DateTime appointmentTime)
        {
            var dayOfWeek = (int)appointmentTime.DayOfWeek.ConsiderSaturdayIsFirstDayOfWeek();
            var dailySchedules = DailySchedules
                .Where(schedule => schedule.DayOfWeek.ConsiderSaturdayIsFirstDayOfWeek() == dayOfWeek)
                .ToList();
            if (dailySchedules.Count == 0) return false;
            return dailySchedules.Select(x => x.DaySchedules.Any(p => p.Contains(appointmentTime))).Any();
        }
    }
}