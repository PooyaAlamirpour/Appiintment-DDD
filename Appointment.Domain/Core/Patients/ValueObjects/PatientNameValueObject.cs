using System.Collections.Generic;
using Appointment.Domain.GenericCore.Abstractions;

namespace Appointment.Domain.Core.Patients.ValueObjects
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