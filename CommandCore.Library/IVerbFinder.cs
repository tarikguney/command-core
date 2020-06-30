using System;
using System.Linq;
using System.Reflection;

namespace CommandCore.Library
{
    public interface IVerbFinder
    {
        Type? FindVerbTypeInExecutingAssembly(string verbName);
    }

    public class VerbFinder  : IVerbFinder
    {
        public Type? FindVerbTypeInExecutingAssembly(string verbName)
        {
            var allTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(a => a.BaseType != null && a.BaseType!.IsGenericType &&
                            a.BaseType.GetGenericTypeDefinition() == typeof(Verb<>)).ToList();
            
            return allTypes.FirstOrDefault(t =>
                t!.Name!.Equals(verbName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}