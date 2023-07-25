namespace Appointment.Contracts.Doctors
{
    public record DefineDoctorRequest(string Name, string Family, DoctorSpeciality Speciality);
}