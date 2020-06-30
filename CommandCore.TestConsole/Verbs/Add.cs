using System;
using CommandCore.Library;
using CommandCore.TestConsole.Options;

namespace CommandCore.TestConsole.Verbs
{
    public class Add : Verb<AddOptions>
    {
        public override void Run()
        {
            Console.WriteLine($"{Options!.FirstName} {Options!.LastName}");
        }
    }
}