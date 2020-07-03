using System;
using System.Linq;
using System.Reflection;
using CommandCore.Library.Attributes;
using CommandCore.Library.Interfaces;

namespace CommandCore.Library
{
    public class OptionsParser : IOptionsParser
    {
        public Type GetAssociatedOptionsType(Type verb)
        {
            return verb.BaseType!.GetGenericArguments()[0];
        }

        public VerbOptionsBase CreatePopulatedOptionsObject(Type optionsType, ParsedVerb parsedVerb)
        {
            var options = (VerbOptionsBase?) Activator.CreateInstance(optionsType);
            var optionProperties =
                optionsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            foreach (var propertyInfo in optionProperties.Where(a => a.CanRead && a.CanWrite))
            {
                var parameterNameAttribute = propertyInfo.GetCustomAttribute<ParameterNameAttribute>();
                var argumentName = parameterNameAttribute?.Name ?? propertyInfo.Name;
                propertyInfo.SetValue(options, parsedVerb.Options![argumentName]);
            }

            return options!;
        }
    }
}