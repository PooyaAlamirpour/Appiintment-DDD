using System.Reflection;

namespace Charisma.Domain
{
    public static class DomainAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}