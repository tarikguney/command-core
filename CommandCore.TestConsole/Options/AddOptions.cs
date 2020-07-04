using CommandCore.Library.PublicBase;

namespace CommandCore.TestConsole.Options
{
    public class AddOptions : VerbOptionsBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}