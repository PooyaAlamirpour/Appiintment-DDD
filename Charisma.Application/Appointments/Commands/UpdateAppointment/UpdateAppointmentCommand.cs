using System;
using Charisma.Application.Common.Messages;
using ErrorOr;

namespace Charisma.Application.Appointments.Commands.UpdateAppointment
{
    public sealed record UpdateAppointmentCommand(string TrackingCode, DateTime AppointmentStartDateTime, 
        int DurationMinutes) : ICommand<ErrorOr<Updated>>;
}