using System;
using System.Collections.Immutable;
using System.Linq;
using Charisma.Domain.Core.Doctors.ValueObjects;

namespace Charisma.Domain.Core.Schedules
{
    public record Schedule(DoctorIdValueObject DoctorId, DayOfWeek DayOfWeek, ImmutableArray<Range<DateTime>> DaySchedules)
    {
        public static ImmutableArray<Schedule> Define(WeeklySchedule request)
        {
            var list = request.DailySchedules.Select(x => 
                new Schedule(x.DoctorId, x.DayOfWeek, x.DaySchedules))
                .ToImmutableArray();
            return list;
        }
    }
}