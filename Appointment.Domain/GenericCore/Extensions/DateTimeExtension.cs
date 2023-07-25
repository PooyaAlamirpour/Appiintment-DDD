using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Appointment.Domain.Core.Schedules;

namespace Appointment.Domain.GenericCore.Extensions
{
    public static class DateTimeExtension
    {
        public static DateOnly ToDateOnly(this DateTime d)
        {
            return new DateOnly(d.Year, d.Month, d.Day);
        }

        public static TimeOnly ToTimeOnly(this DateTime d)
        {
            return new TimeOnly(d.Hour, d.Minute, d.Second, d.Millisecond);
        }
        
        public static int ConsiderSaturdayIsFirstDayOfWeek(this DayOfWeek dayOfWeek)
        {
            var firstDayOfWeek = DayOfWeek.Saturday;
            var diff = (7 + (dayOfWeek - firstDayOfWeek)) % 7;
            if (diff == 0) return 0;
            var day = (diff + (int)firstDayOfWeek) % 7;
            return day + 1;
        }
        
        public static ImmutableArray<Range<DateTime>> SplitChunk(this ImmutableArray<Range<DateTime>> ranges, int chunk)
        {
            List<Range<DateTime>> list = new();
            foreach (var range in ranges)
            {
                var chunkedCount = (int)Math.Ceiling((range.End - range.Start) / TimeSpan.FromMinutes(chunk));
                for (var i = 0; i < chunkedCount; i++)
                {
                    var start = range.Start.AddMinutes(i * chunk);
                    var end = start.AddMinutes(chunk);
                    list.Add(new Range<DateTime>(start, end));
                }
            }

            return list.ToImmutableArray();
        }
        
        public static ImmutableArray<Range<DateTime>> RemoveOverlapWith(this ImmutableArray<Range<DateTime>> ranges, 
            ImmutableArray<Range<DateTime>> mustBeRemoved)
        {
            List<Range<DateTime>> list = new();
            foreach (var range in ranges)
            {
                if (mustBeRemoved.Length == 0) return ranges;
                list.AddRange(from item in mustBeRemoved
                    where !range.Contains(item.Start) 
                    where !range.Contains(item.End) 
                    select range);
            }

            return list.Distinct().ToImmutableArray();
        }
    }
}