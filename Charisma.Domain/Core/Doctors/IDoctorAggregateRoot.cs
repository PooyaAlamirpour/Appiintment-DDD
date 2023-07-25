using Charisma.Domain.Core.Doctors.ValueObjects;
using Charisma.Domain.GenericCore.Interfaces;

namespace Charisma.Domain.Core.Doctors
{
    public interface IDoctorAggregateRoot : IDo<DoctorAggregateRoot, DoctorIdValueObject>
    {
        
    }
}