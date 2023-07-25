using System;
using System.Collections.Generic;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Domain.Core.Appointments.ValueObjects
{
    public class AppointmentHistoryValueObject : ValueObject
    {
        private AppointmentHistoryValueObject(DateTime appointmentTime, TimeSpan appointmentDuration)
        {
            AppointmentTime = appointmentTime;
            AppointmentDuration = appointmentDuration;
        }

        public DateTime AppointmentTime { get; private set; }
        public TimeSpan AppointmentDuration { get; private set; }

        public static AppointmentHistoryValueObject New(DateTime appointmentTime, TimeSpan appointmentDuration)
        {
            return new AppointmentHistoryValueObject(appointmentTime, appointmentDuration);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}