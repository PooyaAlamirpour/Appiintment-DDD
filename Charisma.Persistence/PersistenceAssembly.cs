using System.Reflection;

namespace Charisma.Persistence
{
    public static class PersistenceAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}