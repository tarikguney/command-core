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
            var options = (VerbOptionsBase?) Activator.CreateInstance(verbOptionsType);
            var optionProperties =
                verbOptionsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in optionProperties.Where(a => a.CanRead && a.CanWrite))
            {
                var parameterNameAttribute = propertyInfo.GetCustomAttribute<ParameterNameAttribute>();
                var argumentName = parameterNameAttribute?.Name ?? propertyInfo.Name;
                var argumentValue = parsedVerb.Options![argumentName];
                // Parsing the string value to the type stated by the property type of the Options object.
                var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                propertyInfo.SetValue(options, converter.ConvertFrom(argumentValue));
            }

            return options!;
        }
    }
}