using System;
using Appointment.Application.Common.Messages;
using ErrorOr;

namespace Appointment.Application.Appointments.Commands.UpdateAppointment
{
    public sealed record UpdateAppointmentCommand(string TrackingCode, DateTime AppointmentStartDateTime, 
        int DurationMinutes) : ICommand<ErrorOr<Updated>>;
}