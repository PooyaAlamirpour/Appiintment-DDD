using System;
using Appointment.Application.Common.Messages;
using Appointment.Domain.Core.Salary.ValueObjects;
using ErrorOr;

namespace Appointment.Application.Salary.Queries
{
    public record GetSalaryQuery(Guid DoctorId, int Year, int Month) : IQuery<ErrorOr<SalaryValueObject>>;
}