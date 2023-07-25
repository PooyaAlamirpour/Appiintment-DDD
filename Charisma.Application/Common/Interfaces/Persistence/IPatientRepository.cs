using System;
using System.Threading.Tasks;
using Charisma.Domain.Core.Patients;

namespace Charisma.Application.Common.Interfaces.Persistence
{
    public interface IPatientRepository
    {
        Task<PatientAggregateRoot> GetByIdAsync(Guid id);
    }
}