using System;
using System.Linq;
using System.Reflection;

namespace CommandCore.Library
{
    public class VerbFinder  : IVerbFinder
    {
        public Type? FindVerbTypeInExecutingAssembly(string verbName)
        {
            // TODO using getentryassembly might not be the perfect solution. Needs more testing here.
            var allTypes = Assembly.GetEntryAssembly()!.GetTypes()
                .Where(a => a.BaseType != null && a.BaseType!.IsGenericType &&
                            a.BaseType.GetGenericTypeDefinition() == typeof(Verb<>)).ToList();
            
            return allTypes.FirstOrDefault(t =>
                t!.Name!.Equals(verbName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}