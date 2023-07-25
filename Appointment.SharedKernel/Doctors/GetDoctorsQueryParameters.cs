using Appointment.Contracts.Common;

namespace Appointment.Contracts.Doctors
{
    public class GetDoctorsQueryParameters : PaginationQueryParameters
    {
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public DoctorSpeciality? Speciality { get; set; } = null;
    }
}