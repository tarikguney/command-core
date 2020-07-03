using System;
using CommandCore.Library.Interfaces;

namespace CommandCore.Library
{
    public class CommandCoreApp
    {
        private readonly ICommandParser _commandParser;
        private readonly IVerbTypeFinder _verbTypeFinder;
        private readonly IOptionsParser _optionsParser;

        public CommandCoreApp(ICommandParser commandParser, IVerbTypeFinder verbTypeFinder, IOptionsParser optionsParser)
        {
            _commandParser = commandParser;
            _verbTypeFinder = verbTypeFinder;
            _optionsParser = optionsParser;
        }

        public CommandCoreApp()
        {
            _commandParser = new DummyCommandParser();
            _verbTypeFinder = new VerbTypeFinder();
            _optionsParser = new OptionsParser();
        }
        
        public int Parse(string[] args)
        {
            var parsedVerb = _commandParser.ParseCommand(args);
            var verbType = _verbTypeFinder.FindVerbTypeInExecutingAssembly(parsedVerb.VerbName!);
            var options = _optionsParser.CreatePopulatedOptionsObject(verbType!, parsedVerb);
            var verb = SetOptionsOfVerb(verbType!, options);
            verb.Run();
            
            return 0;
        }

        private IVerbRunner SetOptionsOfVerb(Type verbType, VerbOptionsBase optionsBase)
        {
            var verb = Activator.CreateInstance(verbType);
            var optionsPropertyInfo = verbType.GetProperty("Options");
            optionsPropertyInfo!.SetValue(verb, optionsBase);
            return (IVerbRunner) verb!;
        }
    }
}