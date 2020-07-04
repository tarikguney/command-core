using CommandCore.Library.Interfaces;
using CommandCore.LightIoC;
using IServiceProvider = CommandCore.LightIoC.IServiceProvider;

namespace CommandCore.Library
{
    /// <summary>
    /// CommandCoreApp is the entry point of the CommandCore library. It represents the CommandCore application
    /// and provides the necessary parsing and type finding functionality for internal use. You should start with this
    /// class to start using MVC capabilities.
    /// </summary>
    public class CommandCoreApp
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandCoreApp()
        {
            _serviceProvider = new BasicServiceProvider();
            _serviceProvider.Register<ICommandParser, CommandParser>();
            _serviceProvider.Register<IVerbTypeFinder, VerbTypeFinder>();
            _serviceProvider.Register<IOptionsParser, OptionsParser>();
            _serviceProvider.Register<IEntryAssemblyProvider, BasicEntryAssemblyProvider>();
            _serviceProvider.Register<ICommandCoreVerbRunner, CommandCoreVerbRunner>();
        }

        public int Parse(string[] args)
        {
            return _serviceProvider.Resolve<ICommandCoreVerbRunner>().Run(args);
        }
    }
}