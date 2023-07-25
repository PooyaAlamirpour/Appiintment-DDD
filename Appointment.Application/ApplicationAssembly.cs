using System.Reflection;

namespace Appointment.Application
{
    public static class ApplicationAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}