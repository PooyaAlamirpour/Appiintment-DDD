using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Doctors.ValueObjects;
using Charisma.Domain.SubDomains.Doctors;
using Charisma.Persistence.Common.Converters.Abstracts;
using Charisma.Persistence.Common.Extensions;
using Charisma.Persistence.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Charisma.Persistence.Repositories
{
    internal sealed class DoctorRepository : IDoctorRepository
    {
        private readonly IEntityConvertor _convertor;
        private readonly IAsyncRepository<DoctorEntity, Guid> _doctorRepository;
        
        public DoctorRepository(IAsyncRepository<DoctorEntity, Guid> doctorRepository, IEntityConvertor convertor)
        {
            _doctorRepository = doctorRepository;
            _convertor = convertor;
        }
        
        public async Task<DoctorAggregateRoot> GetByIdAsync(Guid id)
        {
            var doctor = await _doctorRepository.GetWithFilterAsync(x => x.Id == id);
            var attachedSchedules = 
                doctor?.Include(x => x.Schedules);

            return _convertor.ToDomainModel(attachedSchedules?.FirstOrDefault());
        }

        public async Task<Guid> AddAsync(DoctorAggregateRoot doctor, CancellationToken cancellationToken)
        {
            var doctorEntity = _convertor.ToEntity(doctor);
            await _doctorRepository.AddAsync(doctorEntity, cancellationToken);
            return doctorEntity.Id;
        }

        public async Task<Paged<DoctorAggregateRoot>> GetListWithPaginationAsync(string name, string family, DoctorSpeciality? speciality, 
            int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            IQueryable<DoctorEntity>? query = await _doctorRepository.GetWithFilterAsync(null);
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query?.Where(x => x.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(family))
            {
                query = query?.Where(x => x.Family == family);
            }
            if (speciality is not null)
            {
                query = query?.Where(x => x.Speciality == speciality);
            }
            
            var attachedSchedules = query?.Include(x => x.Schedules);
            var doctorPaged = await attachedSchedules?.ToPagedListAsync(pageNumber, pageSize, cancellationToken)!;
            return _convertor.ToDomain(doctorPaged);
        }
    }
}