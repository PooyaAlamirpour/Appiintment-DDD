using Appointment.Application.Common.Extensions;
using Appointment.Domain.GenericCore.Errors;
using FluentValidation;

namespace Appointment.Application.Appointments.Commands.CreateEarliestAppointment
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