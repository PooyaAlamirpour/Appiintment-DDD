using System;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Application.Common.Messages;
using Appointment.Domain.Core.Schedules;
using ErrorOr;

namespace Appointment.Application.Doctors.Commands
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