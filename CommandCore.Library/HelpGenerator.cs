using System.Reflection;
using System.Text;
using CommandCore.Library.Attributes;
using CommandCore.Library.Interfaces;

namespace CommandCore.Library
{
    internal class HelpGenerator : IHelpGenerator
    {
        private readonly IVerbTypeFinder _verbTypeFinder;

        public HelpGenerator(IVerbTypeFinder verbTypeFinder)
        {
            _verbTypeFinder = verbTypeFinder;
        }
        public StringBuilder Build()
        {
            var helpBuilder = new StringBuilder();
            var allTypes = _verbTypeFinder.FindAll();
            helpBuilder.AppendLine("VERBS:");
            helpBuilder.AppendLine("------");
            foreach (var verbType in allTypes)
            {
                var attribute = verbType.GetCustomAttribute<VerbNameAttribute>();
                var verbName = attribute?.Name ?? verbType.Name;
                helpBuilder.AppendLine(
                    $"  - {verbName}: {attribute?.Description}");
                // TODO needs a safer handling here. I wil address this later.
                helpBuilder.AppendLine($"    OPTIONS");
                helpBuilder.AppendLine("    --------");
                foreach (var optionPropertyInfo in verbType.BaseType!.GetGenericArguments()[0].GetProperties())
                {
                    var optionPropertyAttribute = optionPropertyInfo.GetCustomAttribute<ParameterNameAttribute>();
                    var optionName = optionPropertyAttribute?.Name ?? optionPropertyInfo.Name;
                    helpBuilder.AppendLine(
                        $"    - {optionName} (Alias: {optionPropertyAttribute.Alias}): {optionPropertyAttribute.Description}");
                }
            }

            return helpBuilder;
        }
    }
}