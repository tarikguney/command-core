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
        private readonly IHelpGenerator _helpGenerator;

        public CommandCoreVerbRunner(ICommandParser commandParser, IVerbTypeFinder verbTypeFinder,
            IOptionsParser optionsParser, IHelpGenerator helpGenerator)
        {
            _commandParser = commandParser;
            _verbTypeFinder = verbTypeFinder;
            _optionsParser = optionsParser;
            _helpGenerator = helpGenerator;
        }
        
        public int Run(string[] args)
        {
            try
            {
                if (args.Length > 0 && args[0] == "--help")
                {
                    var help = _helpGenerator.Build();
                    Console.WriteLine(help);
                    return 0;
                }
                
                var parsedVerb = _commandParser.ParseCommand(args);
                var verbType = _verbTypeFinder.FindByName(parsedVerb.VerbName!);
                var options = _optionsParser.CreatePopulatedOptionsObject(verbType!, parsedVerb);

                var verbObject = (IVerbRunner) Activator.CreateInstance(verbType!)!;

                if (options != null)
                {
                    var optionsPropertyInfo = verbType!.GetProperty("Options");
                    optionsPropertyInfo!.SetValue(verbObject, options);
                }
                
                VerbViewBase view = verbObject.Run();
                // Running the view to render the result to the console (stdout).
                // This could be a good extension point for various redirections.
                view.RenderResponse();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }
    }
}