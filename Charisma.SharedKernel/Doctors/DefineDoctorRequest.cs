using Charisma.Contracts.Schedules;

namespace Charisma.Contracts.Doctors
{
    public record DefineDoctorRequest(string Name, string Family, DoctorSpeciality Speciality);
}