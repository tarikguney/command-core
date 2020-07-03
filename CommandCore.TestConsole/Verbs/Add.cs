using System;
using CommandCore.Library;
using CommandCore.Library.Attributes;
using CommandCore.TestConsole.Options;

namespace CommandCore.TestConsole.Verbs
{
    [VerbName("add")]
    public class Add : VerbBase<AddOptionsBase>
    {
        public override void Run()
        {
            Console.WriteLine($"{Options!.FirstName} {Options!.LastName}");
        }
    }
}