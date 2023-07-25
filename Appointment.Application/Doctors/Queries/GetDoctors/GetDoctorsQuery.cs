using Appointment.Application.Common.Messages;
using Appointment.Application.Common.Models;
using Appointment.Domain.Core.Doctors;
using Appointment.Domain.SubDomains.Doctors;
using ErrorOr;

namespace Appointment.Application.Doctors.Queries.GetDoctors
{
    public record GetDoctorsQuery(
        string Name, 
        string Family, 
        DoctorSpeciality? Speciality, 
        int PageSize, int PageNumber) : IQuery<ErrorOr<Paged<DoctorAggregateRoot>>>;
}