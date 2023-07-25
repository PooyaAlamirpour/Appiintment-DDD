using System;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.SubDomains.Doctors;

namespace Charisma.Application.Common.Interfaces.Persistence
{
    public interface IDoctorRepository
    {
        Task<DoctorAggregateRoot> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(DoctorAggregateRoot doctor, CancellationToken cancellationToken);

        Task<Paged<DoctorAggregateRoot>> GetListWithPaginationAsync(string name, string family, DoctorSpeciality? speciality,
            int pageSize, int pageNumber, CancellationToken cancellationToken);
    }
}