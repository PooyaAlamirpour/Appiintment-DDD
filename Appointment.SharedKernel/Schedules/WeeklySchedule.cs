using System;
using System.Collections.Immutable;
using System.Linq;

namespace Appointment.Contracts.Schedules
{
    public record WeeklySchedule(ImmutableArray<DailySchedule> DailySchedules)
    {
        public bool AcceptAppointmentTime(DateTime appointmentTime)
        {
            var dayOfWeek = (int)appointmentTime.DayOfWeek;
            var dailySchedule = DailySchedules.SingleOrDefault(schedule => schedule.DayOfWeek == dayOfWeek);
            if (dailySchedule == null) return false;
            return dailySchedule.DaySchedules.Any(p => p.Contains(appointmentTime));
        }
    }
}