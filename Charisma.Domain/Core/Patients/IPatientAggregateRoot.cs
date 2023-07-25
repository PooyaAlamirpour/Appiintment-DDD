using Charisma.Domain.Core.Patients.ValueObjects;
using Charisma.Domain.GenericCore.Interfaces;

namespace Charisma.Domain.Core.Patients
{
    public interface IPatientAggregateRoot : IDo<PatientAggregateRoot, PatientIdValueObject>
    {
        
    }
}