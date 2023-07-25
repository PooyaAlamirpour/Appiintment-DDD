using System;
using Appointment.Application.Common.Messages;
using Appointment.Domain.Core.Appointments.ValueObjects;
using ErrorOr;

namespace Appointment.Application.Appointments.Commands.CreateAppointment
{
    public sealed record CreateAppointmentCommand(Guid DoctorId, Guid PatientId, int DurationMinutes, DateTime StartDateTime) : ICommand<ErrorOr<AppointmentIdValueObject>>;
}