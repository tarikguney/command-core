using CommandCore.Library.Attributes;
using CommandCore.Library.PublicBase;

namespace CommandCore.TestConsole.Options
{
    public class AddOptions : VerbOptionsBase
    {
        [ParameterName("firstname")]
        public string? FirstName { get; set; }
        
        [ParameterName("lastname")]
        public string? LastName { get; set; }
        
        [ParameterName("employed")]
        public bool? Employed { get; set; }
    }
}