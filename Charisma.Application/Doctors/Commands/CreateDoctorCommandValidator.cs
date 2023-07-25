using System;
using Charisma.Application.Common.Extensions;
using Charisma.Domain.GenericCore.Errors;
using FluentValidation;

namespace Charisma.Application.Doctors.Commands
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