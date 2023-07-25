using ErrorOr;

namespace Charisma.Domain.GenericCore.Errors
{
    public static partial class Errors
    {
        public static class Patient
        {
            public static class Id
            {
                public static Error Empty =>
                    Error.Validation(
                        code: "Patient.Id.Empty",
                        description: "Patient Id is required.");
            }
        }
    }
}