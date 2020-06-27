using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandCore.TestConsole.Library
{
    public class CommandCore
    {
        public static int Parse(string[] args)
        {
            var verbTypes = GetVerbTypes();
            Console.WriteLine(verbTypes.Select(a => a.FullName).Aggregate((a, b) => $"{a}, {b}"));

            return 0;
        }

        private static IReadOnlyList<Type> GetVerbTypes() => Assembly.GetExecutingAssembly().GetTypes()
            .Where(a => a.BaseType != null & a.BaseType.IsGenericType &&
                        a.BaseType.GetGenericTypeDefinition() == typeof(Verb<>)).ToList();


    }
}