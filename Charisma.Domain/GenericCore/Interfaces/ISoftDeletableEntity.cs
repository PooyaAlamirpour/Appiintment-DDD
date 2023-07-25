using System;

namespace Charisma.Domain.GenericCore.Interfaces
{
    public interface ISoftDeletableEntity
    {
        DateTime? DeletedOnUtc { get; }
        bool IsDeleted { get; }
    }
}