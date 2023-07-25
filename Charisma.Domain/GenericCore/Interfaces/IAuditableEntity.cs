using System;

namespace Charisma.Domain.GenericCore.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime CreatedOnUtc { get; }
        DateTime? ModifiedOnUtc { get; }
    }
}