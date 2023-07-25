using Appointment.Domain.Core.Patients.ValueObjects;
using Appointment.Domain.GenericCore.Interfaces;

namespace Appointment.Domain.Core.Patients
{
    public interface IPatientAggregateRoot : IDo<PatientAggregateRoot, PatientIdValueObject>
    {
        
    }
}