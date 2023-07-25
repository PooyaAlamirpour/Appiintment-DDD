using System;
using Charisma.Application.Common.Messages;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Salary;
using Charisma.Domain.Core.Salary.ValueObjects;
using ErrorOr;

namespace Charisma.Application.Salary.Queries
{
    public record GetSalaryQuery(Guid DoctorId, int Year, int Month) : IQuery<ErrorOr<SalaryValueObject>>;
}