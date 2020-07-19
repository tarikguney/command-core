using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using CommandCore.Library.Attributes;
using CommandCore.Library.Interfaces;
using CommandCore.Library.PublicBase;

namespace CommandCore.Library
{
    internal class OptionsParser : IOptionsParser
    {
        public VerbOptionsBase CreatePopulatedOptionsObject(Type verbType, ParsedVerb parsedVerb)
        {
            var verbOptionsType = verbType.BaseType!.GetGenericArguments()[0];
            var options = (VerbOptionsBase) Activator.CreateInstance(verbOptionsType)!;
            var optionProperties =
                verbOptionsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (parsedVerb.Options == null)
            {
                return options;
            }

            foreach (var propertyInfo in optionProperties.Where(a => a.CanRead && a.CanWrite))
            {
                var parameterNameAttributes = propertyInfo.GetCustomAttributes<OptionNameAttribute>().ToList();

                string? ParameterNameFunc() =>
                    parameterNameAttributes?.FirstOrDefault(a => parsedVerb.Options.ContainsKey(a.Name))
                        ?.Name;

                string? ParameterAliasFunc() =>
                    parameterNameAttributes.FirstOrDefault(a =>
                            !string.IsNullOrWhiteSpace(a.Alias) && parsedVerb.Options.ContainsKey(a.Alias))
                        ?.Alias;

                var parameterName = ParameterNameFunc()
                                    ?? ParameterAliasFunc()
                                    ?? propertyInfo.Name;


                if (parsedVerb.Options!.ContainsKey(parameterName))
                {
                    var argumentValue = parsedVerb.Options[parameterName];
                    var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                    propertyInfo.SetValue(options, converter.ConvertFrom(argumentValue));
                }
            }

            return options!;
        }
    }
}