using Charisma.Application.Common.Extensions;
using Charisma.Domain.GenericCore.Errors;
using FluentValidation;

namespace Charisma.Application.Appointments.Queries.GetAppointments
{
    public sealed class GetAppointmentsQueryValidator : AbstractValidator<GetAppointmentsQuery>
    {
        public GetAppointmentsQueryValidator()
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