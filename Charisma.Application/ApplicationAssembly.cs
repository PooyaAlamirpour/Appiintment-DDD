using System.Reflection;

namespace Charisma.Application
{
    public static class ApplicationAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}