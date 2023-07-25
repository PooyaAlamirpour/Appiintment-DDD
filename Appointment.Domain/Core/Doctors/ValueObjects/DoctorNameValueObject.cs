using System.Collections.Generic;
using Appointment.Domain.GenericCore.Abstractions;

namespace Appointment.Domain.Core.Doctors.ValueObjects
{
    public class DoctorNameValueObject : ValueObject
    {
        public readonly string Value;
        private DoctorNameValueObject(string name)
        {
            Value = name;
        }

        public static DoctorNameValueObject New(string name)
        {
            return new DoctorNameValueObject(name);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}