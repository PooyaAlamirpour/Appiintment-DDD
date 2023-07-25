using Charisma.Domain.Core.Salary.ValueObjects;
using Charisma.Domain.GenericCore.Interfaces;

namespace Charisma.Domain.Core.Salary
{
    public interface ISalaryAggregateRoot : IDo<SalaryAggregateRoot, SalaryValueObject>
    {
        
    }
}