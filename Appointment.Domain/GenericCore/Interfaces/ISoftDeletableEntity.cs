using System;

namespace Appointment.Domain.GenericCore.Interfaces
{
    public interface ISoftDeletableEntity
    {
        DateTime? DeletedOnUtc { get; }
        bool IsDeleted { get; }
    }
}