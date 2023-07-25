using System;
using Appointment.Application.Common.Messages;
using Appointment.Domain.Core.Appointments.ValueObjects;
using ErrorOr;

namespace Appointment.Application.Appointments.Commands.CreateEarliestAppointment
{
    public sealed record CreateEarliestAppointmentCommand(Guid DoctorId, Guid PatientId, int DurationMinutes) : ICommand<ErrorOr<AppointmentIdValueObject>>;
}