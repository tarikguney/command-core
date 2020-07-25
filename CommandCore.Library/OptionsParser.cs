using System;
using System.Collections;
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
                    var argumentValues = parsedVerb.Options[parameterName];

                    // I am aware of the relative complexity of the following big ass if conditions. I will simplify
                    // it one, hopefully. But the idea is simple: A property type may be an array, a collection, or a scalar type.
                    // And, we are paring them accordingly.
                    var propType = propertyInfo.PropertyType;

                    if (propType == typeof(bool))
                    {
                        propertyInfo.SetValue(options, argumentValues.Count <= 0 || bool.Parse(argumentValues[0]));
                    }

                    // Zero count means something for the boolean properties since what matters is whether the flag is present or not
                    // But for the other properties, it means nothing, so skipping property set since the user might have 
                    // assigned the properties a default value. We would not want to override it with null or default primitive type value.
                    if (argumentValues.Count == 0)
                    {
                        continue;
                    }

                    if (propType.IsArray)
                    {
                        // Creating an instance of a new array using the property's array element type, and setting
                        // the property value with it after filling it with the converted values. 
                        var elementType = propType.GetElementType()!;
                        var array = Array.CreateInstance(elementType, argumentValues.Count);
                        for (var i = 0; i < argumentValues.Count; i++)
                        {
                            array.SetValue(Convert.ChangeType(argumentValues[i], elementType), i);
                        }

                        propertyInfo.SetValue(options, array);
                    }
                    else if (propType.IsGenericType &&
                             (propType.GetGenericTypeDefinition() == typeof(IList<>) ||
                              propType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>) ||
                              propType.GetGenericTypeDefinition().GetInterfaces().Any(a =>
                                  a.IsGenericType && (a.GetGenericTypeDefinition() == typeof(IList<>) ||
                                                      a.GetGenericTypeDefinition() == typeof(IReadOnlyList<>)))))

                    {
                        // Creating an instance of a generic list using the property's generic argument, and setting
                        // the property value with it after filling it with the converted values. 
                        var elementType = propType.GetGenericArguments()[0];
                        var listType = typeof(List<>);
                        var constructedListType = listType.MakeGenericType(elementType);
                        IList instance = (IList) Activator.CreateInstance(constructedListType)!;
                        foreach (var arg in argumentValues)
                        {
                            instance.Add(Convert.ChangeType(arg, elementType));
                        }

                        propertyInfo.SetValue(options, instance);
                    }
                    else
                    {
                        var converter = TypeDescriptor.GetConverter(propType);
                        propertyInfo.SetValue(options, converter.ConvertFrom(argumentValues[0]));
                    }
                }
            }

            return options!;
        }
    }
}