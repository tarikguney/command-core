using CommandCore.Library;

namespace CommandCore.TestConsole.Options
{
    public class AddOptionsBase : VerbOptionsBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}