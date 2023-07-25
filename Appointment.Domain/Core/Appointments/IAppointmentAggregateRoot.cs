using Appointment.Domain.Core.Appointments.ValueObjects;
using Appointment.Domain.GenericCore.Interfaces;

namespace Appointment.Domain.Core.Appointments
{
    public interface IAppointmentAggregateRoot : IDo<AppointmentAggregateRoot, AppointmentIdValueObject>
    {
        
    }
}