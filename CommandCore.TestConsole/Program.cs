using System;
using CommandCore.Library;

namespace CommandCore.TestConsole
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var commandCoreApp = new CommandCoreApp();
            commandCoreApp.ConfigureServices(sp =>
            {
                sp.Register<IOutputWriter, OutputWriter>();
            });
            return commandCoreApp.Parse(args);
        }
    }

    public class OutputWriter : IOutputWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    public interface IOutputWriter
    {
        void Write(string message);
    }
}