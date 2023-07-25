using System.Reflection;

namespace Appointment.Infrastructure
{
    public static class InfrastructureAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}