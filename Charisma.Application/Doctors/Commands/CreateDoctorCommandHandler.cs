using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Messages;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Doctors.ValueObjects;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.GenericCore.Errors;
using ErrorOr;

namespace Charisma.Application.Doctors.Commands
{
    public class CreateDoctorCommandHandler : ICommandHandler<DefineDoctorCommand, ErrorOr<DoctorAggregateRoot>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateDoctorCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<DoctorAggregateRoot>> Handle(DefineDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = DoctorAggregateRoot.Define(request.Name, request.Family, request.Speciality);

            try
            {
                var doctorId = await _doctorRepository.AddAsync(doctor, cancellationToken);
                doctor.SetId(doctorId);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Errors.Doctor.Exception(ex.Message);
            }

            return doctor;
        }
    }
}