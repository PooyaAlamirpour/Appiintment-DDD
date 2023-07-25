using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Messages;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Doctors;
using ErrorOr;

namespace Charisma.Application.Doctors.Queries.GetDoctors
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