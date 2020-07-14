using System.Linq;
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

            if (allTypes == null || allTypes.Count == 0)
            {
                helpBuilder.Append("No help is found!");
                return helpBuilder;
            }

            helpBuilder.AppendLine("VERBS:");
            helpBuilder.AppendLine("----------------------");
            foreach (var verbType in allTypes)
            {
                var attributes = verbType.GetCustomAttributes<VerbNameAttribute>().ToList();
                var verbName = attributes.FirstOrDefault()?.Name ?? verbType.Name;
                helpBuilder.Append(
                    $"{verbName}");
                if (attributes.Count > 1)
                {
                    var alternativeNames = attributes.Skip(1).Select(a => a.Name).Aggregate((a, b) => $"{a},{b}");
                    helpBuilder.Append($" ({alternativeNames}) ");
                }

                // If there is description to show for teh verb, show it after a colon.
                if (!string.IsNullOrWhiteSpace(attributes?.FirstOrDefault()?.Description))
                {
                    helpBuilder.AppendLine($": {attributes?.FirstOrDefault()?.Description}");
                }

                var optionProperties = verbType.BaseType!.GetGenericArguments()[0].GetProperties();
                if (optionProperties.Length == 0)
                {
                    continue;
                }

                helpBuilder.AppendLine($"  Options:");
                foreach (var optionPropertyInfo in optionProperties)
                {
                    var optionsAttributes = optionPropertyInfo.GetCustomAttributes<OptionNameAttribute>().ToList();
                    var firstAttribute = optionsAttributes.FirstOrDefault();

                    var optionName = firstAttribute?.Name ?? optionPropertyInfo.Name;
                    helpBuilder.Append(
                        $"  --{optionName}");
                    if (!string.IsNullOrWhiteSpace(firstAttribute?.Alias))
                    {
                        helpBuilder.Append($" -{firstAttribute!.Alias}");
                    }

                    if (optionsAttributes.Count > 1)
                    {
                        var altOptionNames = optionsAttributes.Skip(1)
                            .Select(a => "--" + a.Name + 
                                         (string.IsNullOrWhiteSpace(a.Alias) ? "" : " -" + a.Alias))
                            .Aggregate((a, b) => $"{a},{b}");
                        helpBuilder.Append($" ({altOptionNames}) ");
                    }

                    if (!string.IsNullOrWhiteSpace(firstAttribute?.Description))
                    {
                        helpBuilder.AppendLine($": {firstAttribute!.Description}");
                    }
                }
            }

            return helpBuilder;
        }
    }
}