using Charisma.Application.Common.Messages;
using ErrorOr;

namespace Charisma.Application.Doctors.Commands
{
    public sealed record CreateDoctorScheduleCommand(Charisma.Domain.Core.Schedules.WeeklySchedule Schedule) : ICommand<ErrorOr<bool>>;

}