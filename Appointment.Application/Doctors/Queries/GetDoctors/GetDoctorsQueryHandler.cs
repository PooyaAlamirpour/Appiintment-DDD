using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Application.Common.Messages;
using Appointment.Application.Common.Models;
using Appointment.Domain.Core.Doctors;
using ErrorOr;

namespace Appointment.Application.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQueryHandler : IQueryHandler<GetDoctorsQuery, ErrorOr<Paged<DoctorAggregateRoot>>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetDoctorsQueryHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Paged<DoctorAggregateRoot>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctorList = await _doctorRepository.GetListWithPaginationAsync(
                request.Name, request.Family, request.Speciality, 
                request.PageSize, request.PageNumber, cancellationToken);

            return doctorList;
        }
    }
}