using Appointment.Domain.Core.Doctors.ValueObjects;
using Appointment.Domain.GenericCore.Interfaces;

namespace Appointment.Domain.Core.Doctors
{
    public interface IDoctorAggregateRoot : IDo<DoctorAggregateRoot, DoctorIdValueObject>
    {
        
    }
}