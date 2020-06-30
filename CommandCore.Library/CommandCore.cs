using System;
using System.Linq;
using System.Reflection;

namespace CommandCore.Library
{
    public static class CommandCore
    {
        public static int Parse(string[] args)
        {
            ICommandParser commandParser = new DummyCommandParser();
            var parsedVerb = commandParser.ParseCommand(args);
            
            IVerbFinder verbTypeFinder = new VerbFinder();
            var verbType = verbTypeFinder.FindVerbTypeInExecutingAssembly(parsedVerb.VerbName!);
            
            // TODO extract the functionality into their own classes for better isolation for unit testing.
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
    }
}