using Appointment.Application.Common.Messages;
using Appointment.Domain.Core.Doctors;
using Appointment.Domain.Core.Doctors.ValueObjects;
using Appointment.Domain.SubDomains.Doctors;
using ErrorOr;

namespace Appointment.Application.Doctors.Commands
{
    public sealed record DefineDoctorCommand(DoctorNameValueObject Name, DoctorFamilyValueObject Family, DoctorSpeciality? Speciality) : ICommand<ErrorOr<DoctorAggregateRoot>>;
}