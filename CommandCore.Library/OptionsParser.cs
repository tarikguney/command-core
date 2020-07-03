using System;
using System.Linq;
using System.Reflection;
using CommandCore.Library.Attributes;
using CommandCore.Library.Interfaces;

namespace CommandCore.Library
{
    public class OptionsParser : IOptionsParser
    {
        public VerbOptionsBase CreatePopulatedOptionsObject(Type verbType, ParsedVerb parsedVerb)
        {
            var verbOptionsType = verbType.BaseType!.GetGenericArguments()[0];
            var options = (VerbOptionsBase?) Activator.CreateInstance(verbOptionsType);
            var optionProperties =
                verbOptionsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
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