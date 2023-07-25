using Appointment.Domain.Core.Salary.ValueObjects;
using Appointment.Domain.GenericCore.Interfaces;

namespace Appointment.Domain.Core.Salary
{
    public interface ISalaryAggregateRoot : IDo<SalaryAggregateRoot, SalaryValueObject>
    {
        
    }
}