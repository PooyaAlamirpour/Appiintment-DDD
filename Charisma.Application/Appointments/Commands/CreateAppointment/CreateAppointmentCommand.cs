using System;
using Charisma.Application.Common.Messages;
using Charisma.Domain.Core.Appointments;
using Charisma.Domain.Core.Appointments.ValueObjects;
using ErrorOr;

namespace Charisma.Application.Appointments.Commands.CreateAppointment
{
    public sealed record CreateAppointmentCommand(Guid DoctorId, Guid PatientId, int DurationMinutes, DateTime StartDateTime) : ICommand<ErrorOr<AppointmentIdValueObject>>;
}