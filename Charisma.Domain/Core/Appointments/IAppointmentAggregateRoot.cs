using Charisma.Domain.Core.Appointments.ValueObjects;
using Charisma.Domain.GenericCore.Interfaces;

namespace Charisma.Domain.Core.Appointments
{
    public interface IAppointmentAggregateRoot : IDo<AppointmentAggregateRoot, AppointmentIdValueObject>
    {
        
    }
}