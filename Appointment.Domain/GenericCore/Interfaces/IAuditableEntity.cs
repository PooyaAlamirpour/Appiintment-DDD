using System;

namespace Appointment.Domain.GenericCore.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime CreatedOnUtc { get; }
        DateTime? ModifiedOnUtc { get; }
    }
}