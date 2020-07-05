using System.Collections.Generic;
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

            var options = new Dictionary<string, string>();

            for (int i = startingPoint; i < arguments.Length; i++)
            {
                if (arguments[i].StartsWith("-"))
                {
                    // Checking if the option is with a value like --name "Tarik" or -n "Tarik"
                    if (arguments.Length - 1 >= i + 1 && !arguments[i + 1].StartsWith("-"))
                    {
                        options[arguments[i].TrimStart('-')] = arguments[i + 1];
                        // We already captured the value which is the next item in the array, so we can skip it.
                        i++;
                    }
                    // Checking if the option is a flag like --visible or -v, which is automatically inferred as --visible
                    // true. If the --argument is the last item or a value does not follow it, it means it is a flag.
                    else if (arguments.Length - 1 == i || arguments[i + 1].StartsWith("-"))
                    {
                        options[arguments[i].TrimStart('-')] = "true";
                    }
                }
            }

            parsedVerb.Options = options;
            return parsedVerb;
        }
    }
}