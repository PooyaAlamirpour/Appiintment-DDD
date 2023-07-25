using Appointment.Application.Common.Messages;
using Appointment.Domain.Core.Schedules;
using ErrorOr;

namespace Appointment.Application.Doctors.Commands
{
    public sealed record CreateDoctorScheduleCommand(WeeklySchedule Schedule) : ICommand<ErrorOr<bool>>;

}