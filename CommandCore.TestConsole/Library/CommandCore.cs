using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandCore.TestConsole.Library
{
    public static class CommandCore
    {
        public static int Parse(string[] args)
        {
            var verbTypes = GetVerbTypes();
            var parsedVerb = GetDummyVerb();
            var verbType = FindVerbTypeByName(parsedVerb.VerbName!, verbTypes);
            var optionsType = GetAssociatedOptionsType(verbType!);
            var options = CreatePopulatedOptionsObject(optionsType, parsedVerb);
            var verb = SetOptionsOfVerb(verbType!, options);
            verb.Run();
            return 0;
        }

        private static IVerb SetOptionsOfVerb(Type verbType, VerbOptions options)
        {
            var verb = Activator.CreateInstance(verbType);
            var optionsPropertyInfo = verbType.GetProperty("Options");
            optionsPropertyInfo!.SetValue(verb, options);
            return (IVerb) verb!;
        }

        private static VerbOptions CreatePopulatedOptionsObject(Type optionsType, ParsedVerb parsedVerb)
        {
            var options = (VerbOptions?) Activator.CreateInstance(optionsType);
            // Todo I am still not a hundred percent sure if the BindingsFlags logic is correct. It works though.
            var optionProperties =
                optionsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in optionProperties.Where(a => a.CanRead && a.CanWrite))
            {
                // TODO: argument must be lower case for both CLI argument and the property name to match.
                propertyInfo.SetValue(options, parsedVerb.Options![propertyInfo.Name]);
            }

            return options!;
        }

        private static Type GetAssociatedOptionsType(Type verb)
        {
            return verb.BaseType!.GetGenericArguments()[0];
        }

        private static Type? FindVerbTypeByName(string verbName, IReadOnlyList<Type> allTypes)
        {
            return allTypes.FirstOrDefault(t =>
                t!.Name!.Equals(verbName, StringComparison.InvariantCultureIgnoreCase));
        }

        private static IReadOnlyList<Type> GetVerbTypes() => Assembly.GetExecutingAssembly().GetTypes()
            .Where(a => a.BaseType != null && a.BaseType!.IsGenericType &&
                        a.BaseType.GetGenericTypeDefinition() == typeof(Verb<>)).ToList();

        private static ParsedVerb GetDummyVerb() => new ParsedVerb()
        {
            Options = new Dictionary<string, string>()
            {
                {"FirstName", "Tarik"},
                {"LastName", "Guney"}
            },
            VerbName = "add"
        };

        public class ParsedVerb
        {
            public string? VerbName { get; set; }

            // ToDo: Ideally the value should be anything. I don't know how I should design this right now. 
            // The reason is simple: Some arguments are flag attributes.
            public IReadOnlyDictionary<string, string>? Options { get; set; }
        }
    }
}