using System;
using Appointment.Application.Common.Interfaces.Infrastructure;

namespace Appointment.Infrastructure.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.Now;

        public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
    }
}