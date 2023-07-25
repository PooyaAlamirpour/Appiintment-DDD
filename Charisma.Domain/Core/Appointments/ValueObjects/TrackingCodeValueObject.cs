using System;
using System.Collections.Generic;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Domain.Core.Appointments.ValueObjects
{
    public class TrackingCodeValueObject : ValueObject
    {
        private readonly Guid _trackingCode;
        public Guid Value => _trackingCode;
        
        private TrackingCodeValueObject(Guid trackingCode)
        {
            _trackingCode = trackingCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static TrackingCodeValueObject New() => new(Guid.NewGuid());
    }
}