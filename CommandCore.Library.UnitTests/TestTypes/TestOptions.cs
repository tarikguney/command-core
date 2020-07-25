using System.Collections.Generic;
using CommandCore.Library.Attributes;
using CommandCore.Library.PublicBase;

namespace CommandCore.Library.UnitTests.TestTypes
{
    internal class TestOptions : VerbOptionsBase
    {
        [OptionName("Name", Alias = "n")]
        [OptionName("fn")]
        public string Name { get; set; }

        [OptionName("Age", Alias = "a")]
        public int Age { get; set; }

        [OptionName("ismale", Alias = "m")]
        [OptionName("im")]
        public bool Male { get; set; }

        public decimal Money { get; set; }
        
        [OptionName("countries")]
        public string[] Countries { get; set; }

        [OptionName("scores")]
        public List<int> Scores { get; set; }
    }

}