using System.Collections.Generic;
using CommandCore.Library.Attributes;
using CommandCore.Library.Interfaces;
using CommandCore.Library.PublicBase;
using Xunit;

namespace CommandCore.Library.UnitTests
{
    public class OptionsParserTest
    {
        private class TestOptions : VerbOptionsBase
        {
            public string Name { get; set; }
            public int Age { get; set; }

            [OptionName("ismale")]
            public bool Male { get; set; }
        }

        private class TestVerb : VerbBase<TestOptions>
        {
            public override VerbViewBase Run()
            {
                throw new System.NotImplementedException();
            }
        }

        [Fact]
        public void When_Every_Thing_Is_Simple_Things_Work_As_Expected()
        {
            IOptionsParser parser = new OptionsParser();
            var optionsObject = (TestOptions) parser.CreatePopulatedOptionsObject(typeof(TestVerb), new ParsedVerb()
            {
                VerbName = "TestVerb",
                Options = new Dictionary<string, string>()
                {
                    {"Name", "tarik"},
                    {"Age", "33"},
                    {"ismale", "true"}
                }
            });
            Assert.NotNull(optionsObject);
            Assert.Equal("tarik", optionsObject.Name);
            Assert.Equal(33, optionsObject.Age);
            Assert.True(optionsObject.Male);
        }

        [Fact]
        public void When_Options_Name_Differ_They_Are_Ignored_During_Parsing()
        {
            IOptionsParser parser = new OptionsParser();
            var optionsObject = (TestOptions) parser.CreatePopulatedOptionsObject(typeof(TestVerb), new ParsedVerb()
            {
                VerbName = "TestVerb",
                Options = new Dictionary<string, string>()
                {
                    {"Name", "tarik"},
                    {"age", "33"},
                    {"ismale", "true"}
                }
            });
            Assert.NotNull(optionsObject);
            Assert.Equal("tarik", optionsObject.Name);
            Assert.Equal(0, optionsObject.Age);
            Assert.True(optionsObject.Male);
        }

        [Fact]
        public void When_Nothing_Is_Passed_Options_Get_Their_Default_Values()
        {
            IOptionsParser parser = new OptionsParser();
            var optionsObject = (TestOptions) parser.CreatePopulatedOptionsObject(typeof(TestVerb), new ParsedVerb()
            {
                VerbName = "TestVerb",
                Options = new Dictionary<string, string>()
            });
            Assert.NotNull(optionsObject);
            Assert.Null(optionsObject.Name);
            Assert.Equal(0, optionsObject.Age);
            Assert.False(optionsObject.Male);
        }
    }
}