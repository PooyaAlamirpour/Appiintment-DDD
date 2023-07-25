using System;
using ErrorOr;

namespace Appointment.Domain.GenericCore.Errors
{
    public static partial class Errors
    {
        public static class Doctor
        {
            public static class Id
            {
                public static Error Empty =>
                    Error.Validation(
                        code: "Doctor.Id.Empty",
                        description: "Doctor Id is required.");
            }
            
            public static Func<string, Error> NotFound => id =>
                Error.NotFound(
                    code: "Doctor.NotFound",
                    description: $"Doctor with ID '{id}' not found");

            public static class Name
            {
                public static Error Empty =>
                    Error.Validation(
                        code: "Doctor.Name.Empty",
                        description: "Doctor Name is required.");
            }

            public static class Family
            {
                public static Error Empty =>
                    Error.Validation(
                        code: "Doctor.Family.Empty",
                        description: "Doctor Family is required.");
            }

            public static Func<string, Error> Exception => detail =>
                Error.Unexpected(
                    code: "Appointment.Exception",
                    description: $"Appointment has faced with Exception: {detail}");
        }
    }
    
}