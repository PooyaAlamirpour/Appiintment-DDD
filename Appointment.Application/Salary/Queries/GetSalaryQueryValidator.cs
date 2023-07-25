using Appointment.Application.Common.Extensions;
using Appointment.Domain.GenericCore.Errors;
using FluentValidation;

namespace Appointment.Application.Salary.Queries
{
    public class GetSalaryQueryValidator : AbstractValidator<GetSalaryQuery>
    {
        public GetSalaryQueryValidator()
        {
            RuleFor(x => x.Month)
                .GreaterThan(0)
                .WithError(Errors.Salary.Date.Empty);

            RuleFor(x => x.Year)
                .GreaterThan(0)
                .WithError(Errors.Salary.Date.Empty);
            
            RuleFor(x => x.DoctorId)
                .NotEmpty()
                .WithError(Errors.Salary.DoctorId.Empty);
        }
    }
}