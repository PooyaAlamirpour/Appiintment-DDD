using System;
using System.Collections.Immutable;

namespace Appointment.Contracts.Schedules
{
    public record DailySchedule(int DayOfWeek, ImmutableArray<Range<DateTime>> DaySchedules);
}