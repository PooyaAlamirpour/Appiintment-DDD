using System;
using Charisma.Application.Common.Messages;
using Charisma.Domain.Core.Appointments.ValueObjects;

using ErrorOr;

namespace Charisma.Application.Appointments.Commands.CreateEarliestAppointment
{
    public sealed record CreateEarliestAppointmentCommand(Guid DoctorId, Guid PatientId, int DurationMinutes) : ICommand<ErrorOr<AppointmentIdValueObject>>;
}