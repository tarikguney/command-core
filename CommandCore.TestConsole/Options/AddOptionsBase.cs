using CommandCore.Library;
using CommandCore.Library.PublicBase;

namespace CommandCore.TestConsole.Options
{
    public class AddOptionsBase : VerbOptionsBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}