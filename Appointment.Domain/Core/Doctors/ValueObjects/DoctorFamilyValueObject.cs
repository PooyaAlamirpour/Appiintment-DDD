using System.Collections.Generic;
using Appointment.Domain.GenericCore.Abstractions;

namespace Appointment.Domain.Core.Doctors.ValueObjects
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