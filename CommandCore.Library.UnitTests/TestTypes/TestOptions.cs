using CommandCore.Library.Attributes;
using CommandCore.Library.PublicBase;

namespace CommandCore.Library.UnitTests.TestTypes
{
    internal class TestOptions : VerbOptionsBase
    {
        [OptionName("Name", Alias = "n")]
        public string Name { get; set; }

        [OptionName("Age", Alias = "a")]
        public int Age { get; set; }

        [OptionName("ismale", Alias = "m")]
        public bool Male { get; set; }

        public decimal Money { get; set; }
    }

}