using System;
using System.Threading.Tasks;
using Appointment.Domain.Core.Patients;

namespace Appointment.Application.Common.Interfaces.Persistence
{
    public interface IPatientRepository
    {
        Task<PatientAggregateRoot> GetByIdAsync(Guid id);
    }
}