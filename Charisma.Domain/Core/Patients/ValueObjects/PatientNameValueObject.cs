using System.Collections.Generic;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Domain.Core.Patients.ValueObjects
{
    public class PatientNameValueObject : ValueObject
    {
        public readonly string Value;
        public PatientNameValueObject(string name)
        {
            Value = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}