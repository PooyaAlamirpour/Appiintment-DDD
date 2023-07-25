using Charisma.Application.Common.Extensions;
using Charisma.Domain.GenericCore.Errors;
using FluentValidation;

namespace Charisma.Application.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQueryValidator : AbstractValidator<GetDoctorsQuery>
    {
        public GetDoctorsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithError(Errors.Common.Pagination.InvalidPageNumber);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithError(Errors.Common.Pagination.InvalidPageSize);
        }
    }
}