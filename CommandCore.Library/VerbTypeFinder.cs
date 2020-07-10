using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandCore.Library.Attributes;
using CommandCore.Library.Interfaces;
using CommandCore.Library.PublicBase;

namespace CommandCore.Library
{
    internal class VerbTypeFinder : IVerbTypeFinder
    {
        private readonly IEntryAssemblyProvider _entryAssemblyProvider;

        public VerbTypeFinder(IEntryAssemblyProvider entryAssemblyProvider)
        {
            _entryAssemblyProvider = entryAssemblyProvider;
        }

        public IReadOnlyList<Type> FindAll()
        {
            // TODO using getentryassembly might not be the perfect solution. Needs more testing here.
            var allTypes = _entryAssemblyProvider.GetEntryAssembly().GetTypes()
                .Where(a => a.BaseType != null && a.BaseType!.IsGenericType &&
                            a.BaseType.GetGenericTypeDefinition() == typeof(VerbBase<>)).ToList();
            return allTypes;
        }

        public Type? FindByName(string verbName)
        {
            if (string.IsNullOrWhiteSpace(verbName))
            {
                throw new InvalidOperationException($"{nameof(verbName)} may not be empty or null.");
            }
            
            // Stopping when the verb is found would be more performing, but this implementation is better for
            // future optimization like caching.
            var allTypes = FindAll();
            
            return allTypes.FirstOrDefault(verbType =>
            {
                var verbNameAttribute = verbType.GetCustomAttribute<VerbNameAttribute>();
                var verbTypeName = verbNameAttribute?.Name ?? verbType.Name;
                // To keep the naming predictable and consistent, making a case-sensitive comparison here.
                return verbTypeName.Equals(verbName);
            });
        }
    }
}