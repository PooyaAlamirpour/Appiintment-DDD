using Appointment.Application.Common.Extensions;
using Appointment.Domain.GenericCore.Errors;
using FluentValidation;

namespace Appointment.Application.Doctors.Commands
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