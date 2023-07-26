using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Appointment.Persistence.Common.Logging
{
    public class LoggingProxy<T> : DispatchProxy
    {
        private T Target { get; set; }
        protected override object? Invoke(MethodInfo? targetObject, object?[]? args)
        {
            Console.WriteLine(new string('-', 20));
            Console.WriteLine($"args: {JsonConvert.SerializeObject(args)}");
            Console.WriteLine(new string('-', 20));
            return targetObject.Invoke(Target, args);
        }

        public static T SetProxy<T>(T target) where T : class
        {
            var proxy = Create<T, LoggingProxy<T>>() as LoggingProxy<T>;
            proxy.Target = target;
            return proxy as T;
        }
    }
}