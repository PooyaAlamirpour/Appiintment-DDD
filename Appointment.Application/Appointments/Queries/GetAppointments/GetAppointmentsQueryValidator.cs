using Appointment.Application.Common.Extensions;
using Appointment.Domain.GenericCore.Errors;
using FluentValidation;

namespace Appointment.Application.Appointments.Queries.GetAppointments
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