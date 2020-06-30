using System;
using System.Linq;
using System.Reflection;

namespace CommandCore.Library
{
    public class OptionsParser : IOptionsParser
    {
        public Type GetAssociatedOptionsType(Type verb)
        {
            return verb.BaseType!.GetGenericArguments()[0];
        }

        public VerbOptions CreatePopulatedOptionsObject(Type optionsType, ParsedVerb parsedVerb)
        {
            var options = (VerbOptions?) Activator.CreateInstance(optionsType);
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