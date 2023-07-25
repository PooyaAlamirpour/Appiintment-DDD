using System.Reflection;

namespace Appointment.Persistence
{
    public static class PersistenceAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}