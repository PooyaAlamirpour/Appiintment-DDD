namespace Charisma.Contracts.Routes
{
    public static partial class ApiRoutes
    {
        public static class Appointment
        {
            public const string Get = "appointments";
            public const string GetById = "appointments/{appointmentId:guid}";
            public const string Create = "appointment";
            public const string CreateEarliestAppointment = "appointment/earliest";
            public const string Update = "appointments/{trackingCode}";
            public const string Delete = "appointments/{appointmentId:guid}";
        }
        
        public static class Doctor
        {
            public const string Get = "doctors";
            public const string GetById = "doctors/{doctorId:guid}";
            public const string Create = "doctors";
            public const string CreateDoctorSchedule = "doctor/{doctorId:guid}/schedules/create";
            public const string Update = "doctors/{doctorId:guid}";
            public const string Delete = "doctors/{doctorId:guid}";
        }
        
        public static class Salary
        {
            public const string GetById = "salary/{doctorId:guid}";
        }
    }
}