using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Messages;
using Charisma.Domain.Core.Schedules;
using ErrorOr;

namespace Charisma.Application.Doctors.Commands
{
    public class CreateDoctorScheduleHandler : ICommandHandler<CreateDoctorScheduleCommand, ErrorOr<bool>>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDoctorScheduleHandler(IScheduleRepository scheduleRepository, IUnitOfWork unitOfWork)
        {
            _scheduleRepository = scheduleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = Schedule.Define(request.Schedule);

            try
            {
                await _scheduleRepository.AddAsync(schedule, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}