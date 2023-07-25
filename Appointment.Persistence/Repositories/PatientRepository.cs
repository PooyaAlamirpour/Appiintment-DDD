using System;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Domain.Core.Patients;
using Appointment.Persistence.Common.Converters.Abstracts;
using Appointment.Persistence.Entities;

namespace Appointment.Persistence.Repositories
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