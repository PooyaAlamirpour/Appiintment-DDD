using System;
using Appointment.Application.Common.Extensions;
using Appointment.Domain.GenericCore.Errors;
using FluentValidation;

namespace Appointment.Application.Appointments.Commands.CreateAppointment
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