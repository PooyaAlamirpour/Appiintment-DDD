using System.Reflection;

namespace Appointment.Domain
{
    public static class DomainAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}