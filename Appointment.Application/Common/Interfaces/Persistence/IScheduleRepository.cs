using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Domain.Core.Schedules;

namespace Appointment.Application.Common.Interfaces.Persistence
{
    public interface IScheduleRepository
    {
        Task AddAsync(ImmutableArray<Schedule> schedules, CancellationToken cancellationToken);
    }
}