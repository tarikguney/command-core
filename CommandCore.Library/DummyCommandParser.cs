using System.Collections.Generic;
using CommandCore.Library.Interfaces;

namespace CommandCore.Library
{
    public class DummyCommandParser : ICommandParser
    {
        public ParsedVerb ParseCommand(string[] arguments) => new ParsedVerb()
        {
            Options = new Dictionary<string, string>()
            {
                {"FirstName", "Tarik"},
                {"LastName", "Guney"}
            },
            VerbName = "add"
        };
    }
}