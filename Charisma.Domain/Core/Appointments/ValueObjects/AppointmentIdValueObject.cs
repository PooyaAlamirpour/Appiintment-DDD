using System;
using System.Collections.Generic;
using Charisma.Domain.Core.Doctors.ValueObjects;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Domain.Core.Appointments.ValueObjects
{
    public sealed class AppointmentIdValueObject : ValueObject
    {
        private readonly DateTime _appointmentDateTime;
        public readonly DoctorIdValueObject DoctorId;
        private Guid _trackingCode;
        private string _appointmentId;
        public string Value => _appointmentId;
        private AppointmentIdValueObject(DateTime appointmentDateTime, DoctorIdValueObject doctorId, Guid trackingCode)
        {
            _appointmentDateTime = appointmentDateTime;
            _trackingCode = trackingCode;
            DoctorId = doctorId;
            _appointmentId = $"{_appointmentDateTime:yyyyMMdd}-{DoctorId.Value}-{_trackingCode}";
        }
        
        private AppointmentIdValueObject(string appointmentId)
        {
            _appointmentId = appointmentId;
        }

        public static AppointmentIdValueObject New(DateTime appointmentDateTime, DoctorIdValueObject doctorId)
        {
            return new AppointmentIdValueObject(appointmentDateTime, doctorId, TrackingCodeValueObject.New().Value);
        }
        
        public static AppointmentIdValueObject New(string appointmentId)
        {
            return new AppointmentIdValueObject(appointmentId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}