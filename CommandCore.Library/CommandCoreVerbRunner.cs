using System;
using CommandCore.Library.Interfaces;
using CommandCore.Library.PublicBase;

namespace CommandCore.Library
{
    /// <summary>
    /// This is the internal class that mainly an abstraction layer for injectable dependencies that start
    /// the application.
    /// </summary>
    internal class CommandCoreVerbRunner : ICommandCoreVerbRunner
    {
        private readonly ICommandParser _commandParser;
        private readonly IVerbTypeFinder _verbTypeFinder;
        private readonly IOptionsParser _optionsParser;

        public CommandCoreVerbRunner(ICommandParser commandParser, IVerbTypeFinder verbTypeFinder,
            IOptionsParser optionsParser)
        {
            _commandParser = commandParser;
            _verbTypeFinder = verbTypeFinder;
            _optionsParser = optionsParser;
        }
        
        public int Run(string[] args)
        {
            try
            {
                var parsedVerb = _commandParser.ParseCommand(args);
                var verbType = _verbTypeFinder.FindVerbTypeInExecutingAssembly(parsedVerb.VerbName!);
                var options = _optionsParser.CreatePopulatedOptionsObject(verbType!, parsedVerb);
                var verb = SetOptionsOfVerb(verbType!, options);
                verb.Run();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
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