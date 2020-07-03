using System;
using CommandCore.Library.Interfaces;

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

            IOptionsParser optionsParser = new OptionsParser();

            var optionsType = optionsParser.GetAssociatedOptionsType(verbType!);
            var options = optionsParser.CreatePopulatedOptionsObject(optionsType, parsedVerb);
            
            var verb = SetOptionsOfVerb(verbType!, options);
            verb.Run();
            
            return 0;
        }

        private static IVerb SetOptionsOfVerb(Type verbType, VerbOptionsBase optionsBase)
        {
            var verb = Activator.CreateInstance(verbType);
            var optionsPropertyInfo = verbType.GetProperty("Options");
            optionsPropertyInfo!.SetValue(verb, optionsBase);
            return (IVerb) verb!;
        }
    }
}