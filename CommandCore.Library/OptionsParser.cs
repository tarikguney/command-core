using System;
using System.Collections.Generic;
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

                    var propType = propertyInfo.PropertyType;
                    if (propType.IsArray)
                    {
                        var elType = propType.GetElementType();
                        var array = Array.CreateInstance(elType, argumentValue.Count);
                        for (var i = 0; i < argumentValue.Count; i++)
                        {
                            array.SetValue(Convert.ChangeType(argumentValue[i], elType), i);
                        }
                        propertyInfo.SetValue(options, array);
                    }
                    else if (propType.IsSubclassOf(typeof(IEnumerable<>)))
                    {
                        var elType = propType.GetGenericArguments()[0];
                        var converter = TypeDescriptor.GetConverter(elType);
                        var value = argumentValue.Select(a => converter.ConvertFrom(a)).ToList();
                        propertyInfo.SetValue(options, value);
                    }
                    else if (propType == typeof(bool))
                    {
                        propertyInfo.SetValue(options, argumentValue.Count <= 0 || bool.Parse(argumentValue[0]));
                    }
                    else
                    {
                        var converter = TypeDescriptor.GetConverter(propType);
                        propertyInfo.SetValue(options, converter.ConvertFrom(argumentValue[0]));
                    }
                }
            }

            return options!;
        }
    }
}