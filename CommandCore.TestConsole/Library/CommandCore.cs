using System;
using System.Linq;
using System.Reflection;

namespace CommandCore.TestConsole.Library
{
    public class CommandCore
    {
        public static int Parse(string[] args)
        {
            var verbTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(a => a.BaseType?.GetGenericTypeDefinition() == typeof(Verb<>));
            
            return 0;
        }
    }
}