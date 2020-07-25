using System.Collections.Generic;
using System.Linq;
using CommandCore.Library.Interfaces;

namespace CommandCore.Library
{
    public class CommandParser : ICommandParser
    {
        public ParsedVerb ParseCommand(string[] arguments)
        {
            if (arguments == null)
            {
                return new ParsedVerb() {VerbName = "default"};
            }

            var argumentsClone = (string[]) arguments.Clone();
            var parsedVerb = new ParsedVerb();

            var firstArgIsVerb = argumentsClone.Length > 0 && !argumentsClone[0].StartsWith("-");
            var startingPoint = 0;
            if (firstArgIsVerb)
            {
                startingPoint = 1;
                parsedVerb.VerbName = arguments[0];
            }
            else
            {
                // If there is no verb defined, we need a default verb that handles all of the coming requests.
                parsedVerb.VerbName = "default";
            }

            if (argumentsClone.Length <= 1)
            {
                return parsedVerb;
            }

            var options = new Dictionary<string, List<string>>();

            for (int i = startingPoint; i < arguments.Length; i++)
            {
                var arg = arguments[i];
                if (arg.StartsWith("-") || arg.StartsWith("--"))
                {
                    options.Add(arg.Trim('-'), new List<string>());
                }
                else
                {
                    options[options.Keys.Last()].Add(arg);
                }
            }

            parsedVerb.Options = options;
            return parsedVerb;
        }
    }
}