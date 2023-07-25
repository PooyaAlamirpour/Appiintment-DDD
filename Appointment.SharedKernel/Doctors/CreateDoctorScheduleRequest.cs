using Appointment.Contracts.Schedules;

namespace Appointment.Contracts.Doctors
{
    public record CreateDoctorScheduleRequest(WeeklySchedule Schedule);
}