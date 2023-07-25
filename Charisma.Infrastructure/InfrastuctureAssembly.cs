using System.Reflection;

namespace Charisma.Infrastructure
{
    public static class InfrastructureAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}