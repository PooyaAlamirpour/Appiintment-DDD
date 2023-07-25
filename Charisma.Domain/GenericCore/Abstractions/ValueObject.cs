using System;
using System.Collections.Generic;
using System.Linq;

namespace Charisma.Domain.GenericCore.Abstractions
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);

        public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);

        public bool Equals(ValueObject? other) => Equals((object?)other);

        public override bool Equals(object? obj) =>
            obj is ValueObject other
            && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

        public override int GetHashCode() =>
            GetEqualityComponents().Select((obj) => obj.GetHashCode()).Aggregate((x, y) => x ^ y);

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}