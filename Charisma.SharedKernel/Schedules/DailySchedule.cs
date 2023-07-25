using System;
using System.Collections.Immutable;

namespace Charisma.Contracts.Schedules
{
    public record DailySchedule(int DayOfWeek, ImmutableArray<Range<DateTime>> DaySchedules);
}