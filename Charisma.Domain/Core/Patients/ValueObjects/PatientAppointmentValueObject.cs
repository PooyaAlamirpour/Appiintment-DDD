using System.Collections.Generic;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Domain.Core.Patients.ValueObjects
{
    public class PatientAppointmentValueObject : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
        
        
    }
}