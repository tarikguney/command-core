using System;

namespace CommandCore.TestConsole
{
    class Program
    {
        static int Main(string[] args)
        {
           return Library.CommandCore.Parse(args);
        }
    }
}