using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Domain.Core.Schedules;

namespace Charisma.Application.Common.Interfaces.Persistence
{
    public interface IScheduleRepository
    {
        Task AddAsync(ImmutableArray<Schedule> schedules, CancellationToken cancellationToken);
    }
}