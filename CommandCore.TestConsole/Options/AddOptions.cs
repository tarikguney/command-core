using CommandCore.Library.Attributes;
using CommandCore.Library.PublicBase;

namespace CommandCore.TestConsole.Options
{
    public class AddOptions : VerbOptionsBase
    {
        [ParameterName("firstname", Description = "First name of the person provided.")]
        public string? FirstName { get; set; }
        
        [ParameterName("lastname", Description = "Last name of the person provided.")]
        public string? LastName { get; set; }
        
        [ParameterName("haslicense", Alias="hs", Description = "Indicates whether the person has a driver license")]
        public bool? HasLicense { get; set; }
        
        [ParameterName("age", Alias = "a")]
        public int Age { get; set; }
    }
}