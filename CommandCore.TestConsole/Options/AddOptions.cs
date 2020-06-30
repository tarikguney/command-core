using CommandCore.Library;

namespace CommandCore.TestConsole.Options
{
    public class AddOptions : VerbOptions
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}