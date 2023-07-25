using System.Collections.Generic;
using Appointment.Domain.GenericCore.Abstractions;

namespace Appointment.Domain.Core.Patients.ValueObjects
{
    public class PatientAppointmentValueObject : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
        
        
    }
}