using Charisma.Application.Common.Extensions;
using Charisma.Domain.GenericCore.Errors;
using FluentValidation;

namespace Charisma.Application.Doctors.Commands
{
    public class CreateDoctorScheduleValidator : AbstractValidator<CreateDoctorScheduleCommand>
    {
        public CreateDoctorScheduleValidator()
        {
            RuleFor(x => x.Schedule)
                .NotEmpty()
                .WithError(Errors.Doctor.Id.Empty);
        }
    }
}