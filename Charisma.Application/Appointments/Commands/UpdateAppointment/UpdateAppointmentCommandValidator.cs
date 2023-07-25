using System;
using Charisma.Application.Common.Extensions;
using Charisma.Domain.GenericCore.Errors;
using FluentValidation;

namespace Charisma.Application.Appointments.Commands.UpdateAppointment
{
    public sealed class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(x => x.TrackingCode)
                .NotEmpty()
                .WithError(Errors.Appointment.Id.Empty)
                .NotEqual(string.Empty)
                .WithError(Errors.Appointment.Id.Empty);

            RuleFor(x => x.AppointmentStartDateTime)
                .Must(x => x > DateTime.Now)
                .WithError(Errors.Appointment.StartDateTime.NotPass);
            
            RuleFor(x => x.DurationMinutes)
                .NotEmpty()
                .WithError(Errors.Appointment.DurationMinutes.Zero)
                .WithError(Errors.Appointment.DurationMinutes.Zero);
        }
    }
}