using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Appointment.Persistence.Common.Converters
{
    internal sealed class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
        {
        }
    }
}