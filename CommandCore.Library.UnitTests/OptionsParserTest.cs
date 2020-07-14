using System.Collections.Generic;
using CommandCore.Library.Interfaces;
using CommandCore.Library.UnitTests.TestTypes;
using Xunit;

namespace CommandCore.Library.UnitTests
{
    public class OptionsParserTest
    {
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

        [Fact]
        public void When_All_Options_Are_Alias_They_Are_Parsed()
        {
            IOptionsParser parser = new OptionsParser();
            var optionsObject = (TestOptions) parser.CreatePopulatedOptionsObject(typeof(TestVerb), new ParsedVerb()
            {
                VerbName = "TestVerb",
                Options = new Dictionary<string, string>()
                {
                    {"n", "tarik"},
                    {"a", "33"},
                    {"m", "true"}
                }
            });
            Assert.NotNull(optionsObject);
            Assert.Equal("tarik", optionsObject.Name);
            Assert.Equal(33, optionsObject.Age);
            Assert.True(optionsObject.Male);
        }

        [Fact]
        public void When_There_Is_No_Option_Name_Mapping_Then_Property_Name_Is_Used()
        {
            IOptionsParser optionsParser = new OptionsParser();
            var optionsObject = (TestOptions) optionsParser.CreatePopulatedOptionsObject(typeof(TestVerb),
                new ParsedVerb()
                {
                    VerbName = "TestVerb",
                    Options = new Dictionary<string, string>
                    {
                        {"Money", "12.55"}
                    }
                });

            Assert.NotNull(optionsObject);
            Assert.Equal(12.55m, optionsObject.Money);
        }

        [Fact]
        public void When_There_Are_Multiple_Option_Bindings_One_Of_Them_Got_Selected()
        {
            IOptionsParser parser = new OptionsParser();
            var optionsObject = (TestOptions) parser.CreatePopulatedOptionsObject(typeof(TestVerb), new ParsedVerb()
            {
                VerbName = "TestVerb",
                Options = new Dictionary<string, string>()
                {
                    {"fn", "tarik"},
                    {"a", "33"},
                    {"im", "true"}
                }
            });
            Assert.NotNull(optionsObject);
            Assert.Equal("tarik", optionsObject.Name);
            Assert.Equal(33, optionsObject.Age);
            Assert.True(optionsObject.Male);
        }
    }
}