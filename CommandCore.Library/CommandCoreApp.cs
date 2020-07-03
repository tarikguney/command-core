using CommandCore.Library.Interfaces;
using CommandCore.LightIoC;
using IServiceProvider = CommandCore.LightIoC.IServiceProvider;

namespace CommandCore.Library
{
    public class CommandCoreApp
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandCoreApp()
        {
            _serviceProvider = new BasicServiceProvider();
            _serviceProvider.Register<ICommandParser, DummyCommandParser>();
            _serviceProvider.Register<IVerbTypeFinder, VerbTypeFinder>();
            _serviceProvider.Register<IOptionsParser, OptionsParser>();
            _serviceProvider.Register<IEntryAssemblyProvider, BasicEntryAssemblyProvider>();
            _serviceProvider.Register<ICommandCodeVerbRunner, CommandCodeVerbRunner>();
        }

        public int Parse(string[] args)
        {
            return _serviceProvider.Resolve<ICommandCodeVerbRunner>().Run(args);
        }
    }
}