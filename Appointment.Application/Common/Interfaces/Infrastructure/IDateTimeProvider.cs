using System;

namespace Appointment.Application.Common.Interfaces.Infrastructure
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateOnly Today { get; }
    }
}