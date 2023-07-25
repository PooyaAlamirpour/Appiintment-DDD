using Charisma.Application.Common.Messages;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Doctors.ValueObjects;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.SubDomains.Doctors;
using ErrorOr;

namespace Charisma.Application.Doctors.Commands
{
    public sealed record DefineDoctorCommand(DoctorNameValueObject Name, DoctorFamilyValueObject Family, DoctorSpeciality? Speciality) : ICommand<ErrorOr<DoctorAggregateRoot>>;
}