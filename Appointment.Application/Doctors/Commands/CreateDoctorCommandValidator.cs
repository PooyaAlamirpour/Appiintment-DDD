using Appointment.Application.Common.Extensions;
using Appointment.Domain.GenericCore.Errors;
using FluentValidation;

namespace Appointment.Application.Doctors.Commands
{
    public class CreateDoctorCommandValidator : AbstractValidator<DefineDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithError(Errors.Doctor.Name.Empty);

            RuleFor(x => x.Family)
                .NotEmpty()
                .WithError(Errors.Doctor.Family.Empty);
        }
    }
}