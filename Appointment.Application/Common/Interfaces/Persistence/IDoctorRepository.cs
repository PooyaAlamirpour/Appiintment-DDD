using System;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Models;
using Appointment.Domain.Core.Doctors;
using Appointment.Domain.SubDomains.Doctors;

namespace Appointment.Application.Common.Interfaces.Persistence
{
    public interface IDoctorRepository
    {
        Task<DoctorAggregateRoot> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(DoctorAggregateRoot doctor, CancellationToken cancellationToken);

        Task<Paged<DoctorAggregateRoot>> GetListWithPaginationAsync(string name, string family, DoctorSpeciality? speciality,
            int pageSize, int pageNumber, CancellationToken cancellationToken);
    }
}