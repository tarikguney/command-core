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
        public VerbOptionsBase? CreatePopulatedOptionsObject(Type verbType, ParsedVerb parsedVerb)
        {
            var verbOptionsType = verbType.BaseType!.GetGenericArguments()[0];
            var options = (VerbOptionsBase?) Activator.CreateInstance(verbOptionsType);
            var optionProperties =
                verbOptionsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (parsedVerb.Options == null)
            {
                return null;
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

                var argumentValue = parsedVerb.Options!.ContainsKey(parameterName)
                    ? parsedVerb.Options[parameterName]
                    : GetDefaultValue(propertyInfo.PropertyType);

                //var argumentValue = parsedVerb.Options![parameterName];
                // Parsing the string value to the type stated by the property type of the Options object.
                var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                // Doing ToString() because converter cannot convert from same type to same type such as int to int.
                // Check if it is null, then no need to convert it since int cannot be null.
                propertyInfo.SetValue(options,
                    argumentValue == null ? null : converter.ConvertFrom(argumentValue.ToString()));
            }

            return options!;
        }

        private object? GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }
    }
}