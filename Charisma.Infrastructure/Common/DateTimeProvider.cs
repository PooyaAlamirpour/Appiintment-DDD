using System;
using Charisma.Application.Common.Interfaces.Infrastructure;

namespace Charisma.Infrastructure.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.Now;

        public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
    }
}