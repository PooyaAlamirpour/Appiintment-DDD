using Charisma.Application.Common.Messages;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.SubDomains.Doctors;
using ErrorOr;

namespace Charisma.Application.Doctors.Queries.GetDoctors
{
    public record GetDoctorsQuery(
        string Name, 
        string Family, 
        DoctorSpeciality? Speciality, 
        int PageSize, int PageNumber) : IQuery<ErrorOr<Paged<DoctorAggregateRoot>>>;
}