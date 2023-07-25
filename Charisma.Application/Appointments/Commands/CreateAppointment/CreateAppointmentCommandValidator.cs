using System;
using Charisma.Application.Common.Extensions;
using Charisma.Domain.GenericCore.Errors;
using FluentValidation;

namespace Charisma.Application.Appointments.Commands.CreateAppointment
{
    public sealed class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentValidator()
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
            
            RuleFor(x => x.StartDateTime)
                .Must(x => x > DateTime.Now)
                .WithError(Errors.Appointment.StartDateTime.NotPass);
        }
    }
}