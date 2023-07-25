using System;
using System.Collections.Generic;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Domain.Core.Patients.ValueObjects
{
    public class PatientIdValueObject : ValueObject
    {
        private readonly Guid _patientId;
        public Guid Value => _patientId;
        
        private PatientIdValueObject(Guid patientId)
        {
            _patientId = patientId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static PatientIdValueObject New(Guid patientId) => new(patientId);
    }
}