using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Domain.Core.Schedules;
using Appointment.Persistence.Common.Converters.Abstracts;
using Appointment.Persistence.Entities;

namespace Appointment.Persistence.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IEntityConvertor _convertor;
        private readonly IAsyncRepository<ScheduleEntity, long> _scheduleRepository;

        public ScheduleRepository(IAsyncRepository<ScheduleEntity, long> scheduleRepository, IEntityConvertor convertor)
        {
            _scheduleRepository = scheduleRepository;
            _convertor = convertor;
        }

        public async Task AddAsync(ImmutableArray<Schedule> schedules, CancellationToken cancellationToken)
        {
            var entityList = _convertor.ToEntity(schedules);
            await _scheduleRepository.AddRangeAsync(entityList, cancellationToken);
        }
    }
}