using System;
using Charisma.Application.Common.Extensions;
using Charisma.Domain.GenericCore.Errors;
using FluentValidation;

namespace Charisma.Application.Appointments.Commands.CreateEarliestAppointment
{
    public class CreateEarliestAppointmentValidator : AbstractValidator<CreateEarliestAppointmentCommand>
    {
        public CreateEarliestAppointmentValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty()
                .WithError(Errors.Doctor.Id.Empty);

            RuleFor(x => x.PatientId)
                .NotEmpty()
                .WithError(Errors.Patient.Id.Empty);

            RuleFor(x => x.DurationMinutes)
                .Must(x => x > 0)
                .WithError(Errors.Duration.Time.Negative);
        }
    }
}