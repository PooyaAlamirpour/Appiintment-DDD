using System;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Domain.Core.Patients;
using Charisma.Persistence.Common.Converters.Abstracts;
using Charisma.Persistence.Entities;

namespace Charisma.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IEntityConvertor _convertor;
        private readonly IAsyncRepository<PatientEntity, Guid> _doctorRepository;

        public PatientRepository(IEntityConvertor convertor, IAsyncRepository<PatientEntity, Guid> doctorRepository)
        {
            _convertor = convertor;
            _doctorRepository = doctorRepository;
        }

        public Task<PatientAggregateRoot> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}