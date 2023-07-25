using ErrorOr;

namespace Appointment.Domain.GenericCore.Errors
{
    public partial class Errors
    {
        public static class Salary
        {
            public static class DoctorId
            {
                public static Error Empty =>
                    Error.Validation(
                        code: "Salary.DoctorId.Empty",
                        description: "DoctorId is required.");
            }
            
            public static class Date
            {
                public static Error Empty =>
                    Error.Validation(
                        code: "Salary.Date.Empty",
                        description: "Salary Date is required.");
            }
        }
    }
}