using System;
using CommandCore.Library.Attributes;
using CommandCore.Library.PublicBase;
using CommandCore.TestConsole.Options;

namespace CommandCore.TestConsole.Verbs
{
    [VerbName("add")]
    public class Add : VerbBase<AddOptions>
    {
        public override void Run()
        {
            Console.WriteLine($"{Options!.FirstName} {Options!.LastName}");
        }
    }
}