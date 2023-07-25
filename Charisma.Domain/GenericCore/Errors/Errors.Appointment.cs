using System;
using Charisma.Domain.Core.Appointments.ValueObjects;
using ErrorOr;

namespace Charisma.Domain.GenericCore.Errors
{
    public static partial class Errors
    {
        public static class Appointment
        {
            public static class Id
            {
                public static Error Empty =>
                    Error.Validation(
                        code: "Appointment.Id.Empty",
                        description: "Appointment ID is required.");
            }

            public static Func<string, Error> NotFound => id =>
                Error.NotFound(
                    code: "Appointment.NotFound",
                    description: $"Appointment with ID '{id}' not found");
            
            public static Func<string, Error> Exception => detail =>
                Error.Unexpected(
                    code: "Appointment.Exception",
                    description: $"Appointment has faced with Exception: {detail}");

            public static class StartDateTime
            {
                public static Error NotPass =>
                    Error.Validation(
                        code: "Appointment.StartDateTime.NotPass",
                        description: "Time for an appointment must not passed.");
            }
            
            public static class DurationMinutes
            {
                public static Error Zero =>
                    Error.Validation(
                        code: "Appointment.DurationMinutes.Zero",
                        description: "Duration for an appointment must not be zero.");
            }
            
        }
    }
}