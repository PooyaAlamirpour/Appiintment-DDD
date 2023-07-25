using ErrorOr;

namespace Charisma.Domain.GenericCore.Errors
{
    public static partial class Errors
    {
        public static class Duration
        {
            public static Error NotPass =>
                Error.Validation(
                    code: "Appointment.StartDateTime.NotPass",
                    description: "Time for an appointment must not passed.");

            public class Time
            {
                public static Error Negative =>
                    Error.Validation(
                        code: "Appointment.Duration.Time",
                        description: "Duration time must be positive");
            }
        }
    }
}