using System;
using Appointment.Application.Common.Extensions;
using Appointment.Domain.GenericCore.Errors;
using FluentValidation;

namespace Appointment.Application.Appointments.Commands.UpdateAppointment
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