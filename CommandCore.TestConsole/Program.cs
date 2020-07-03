using CommandCore.Library;

namespace CommandCore.TestConsole
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var commandCoreApp = new CommandCoreApp();
            return commandCoreApp.Parse(args);
        }
    }
}