using System.Collections.Generic;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Domain.Core.Doctors.ValueObjects
{
    public sealed class DoctorFamilyValueObject : ValueObject
    {
        public readonly string Value;
        private DoctorFamilyValueObject(string family)
        {
            Value = family;
        }

        public static DoctorFamilyValueObject New(string family)
        {
            return new DoctorFamilyValueObject(family);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}