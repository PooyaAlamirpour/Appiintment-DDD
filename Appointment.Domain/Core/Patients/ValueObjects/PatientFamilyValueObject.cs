using System.Collections.Generic;
using Appointment.Domain.GenericCore.Abstractions;

namespace Appointment.Domain.Core.Patients.ValueObjects
{
    public class PatientFamilyValueObject : ValueObject
    {
        public readonly string Value;
        private PatientFamilyValueObject(string family)
        {
            Value = family;
        }

        public static PatientFamilyValueObject New(string family)
        {
            return new PatientFamilyValueObject(family);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}