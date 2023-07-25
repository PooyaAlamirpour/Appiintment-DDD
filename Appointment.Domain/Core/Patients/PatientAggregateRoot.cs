using System;
using System.Threading.Tasks;
using Appointment.Domain.Core.Patients.ValueObjects;
using Appointment.Domain.GenericCore.Abstractions;
using Appointment.Domain.GenericCore.Interfaces;

namespace Appointment.Domain.Core.Patients
{
    public class PatientAggregateRoot : AggregateRoot<PatientIdValueObject>, IAuditableEntity, ISoftDeletableEntity, IPatientAggregateRoot
    {
        public DateTime CreatedOnUtc { get; }
        public DateTime? ModifiedOnUtc { get; }
        public DateTime? DeletedOnUtc { get; }
        public bool IsDeleted { get; }

        public PatientIdValueObject PatientId { get; private set; }
        public PatientNameValueObject Name { get; private set;  }
        public PatientFamilyValueObject Family { get; private set;  }
        public PatientAppointmentValueObject? Appointment { get; private set;  }

        public PatientAggregateRoot(PatientIdValueObject patientId, PatientNameValueObject name,
            PatientFamilyValueObject family, PatientAppointmentValueObject? appointment)
        {
            PatientId = patientId;
            Name = name;
            Family = family;
            Appointment = appointment;
        }

        public static PatientAggregateRoot Define(PatientIdValueObject patientId, PatientNameValueObject name, PatientFamilyValueObject family, PatientAppointmentValueObject? appointment)
        {
            return new PatientAggregateRoot(patientId, name, family, appointment);
        }

        public Task<PatientIdValueObject> Do(PatientAggregateRoot arg)
        {
            throw new NotImplementedException();
        }
    }
}