using System;

namespace Charisma.Persistence.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this TimeOnly time)
        {
            return new DateTime(TimeSpan.TicksPerDay + time.Ticks, DateTimeKind.Utc);
        }
        
        public static int ConsiderSaturdayIsFirstDayOfWeek(this DayOfWeek dayOfWeek)
        {
            var firstDayOfWeek = DayOfWeek.Saturday;
            var diff = (7 + (dayOfWeek - firstDayOfWeek)) % 7;
            var tmp = (diff + (int)firstDayOfWeek) % 7;
            return tmp;
        }
    }
}