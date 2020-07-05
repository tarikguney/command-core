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

            if (allTypes.Count == 0)
            {
                return helpBuilder;
            }
            
            helpBuilder.AppendLine("VERBS:");
            helpBuilder.AppendLine("----------------------");
            foreach (var verbType in allTypes)
            {
                var attribute = verbType.GetCustomAttribute<VerbNameAttribute>();
                var verbName = attribute?.Name ?? verbType.Name;
                helpBuilder.Append(
                    $"{verbName}");
                // If there is description to show for teh verb, show it after a colon.
                if (!string.IsNullOrWhiteSpace(attribute?.Description))
                {
                    helpBuilder.AppendLine($": {attribute?.Description}");
                }
                var optionProperties = verbType.BaseType!.GetGenericArguments()[0].GetProperties();
                if (optionProperties.Length == 0)
                {
                    continue;
                }
                helpBuilder.AppendLine($"  Options:");
                foreach (var optionPropertyInfo in  optionProperties)
                {
                    var optionPropertyAttribute = optionPropertyInfo.GetCustomAttribute<OptionNameAttribute>();
                    var optionName = optionPropertyAttribute?.Name ?? optionPropertyInfo.Name;
                    helpBuilder.Append(
                        $"  --{optionName}");
                    if (!string.IsNullOrWhiteSpace(optionPropertyAttribute?.Alias))
                    {
                        helpBuilder.Append($" (-{optionPropertyAttribute!.Alias})");
                    }

                    if (!string.IsNullOrWhiteSpace(optionPropertyAttribute?.Description))
                    {
                        helpBuilder.AppendLine($": {optionPropertyAttribute!.Description}");
                    }
                }
            }

            return helpBuilder;
        }
    }
}