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
                Options = new Dictionary<string, List<string>>()
                {
                    {"Name", new List<string> {"tarik"}},
                    {"Age", new List<string> {"33"}},
                    {"ismale", new List<string> {"true"}},
                    {"countries", new List<string> {"usa", "germany", "turkey"}},
                    {"scores", new List<string> {"2", "3", "4"}},
                    {"skills", new List<string> {"programming"}},
                    {"ids", new List<string>()}
                }
            });
            Assert.NotNull(optionsObject);
            Assert.Equal("tarik", optionsObject.Name);
            Assert.Equal(33, optionsObject.Age);
            Assert.True(optionsObject.Male);
            Assert.Equal(new List<string> {"usa", "germany", "turkey"}, optionsObject.Countries);
            Assert.Equal(new List<int> {2, 3, 4}, optionsObject.Scores);
            IReadOnlyList<string> expectedSkills = new List<string>() {"programming"};
            Assert.Equal(expectedSkills, optionsObject.Skills);
            Assert.Null(optionsObject.Ids);
        }

        [Fact]
        public void When_Options_Name_Differ_They_Are_Ignored_During_Parsing()
        {
            IOptionsParser parser = new OptionsParser();
            var optionsObject = (TestOptions) parser.CreatePopulatedOptionsObject(typeof(TestVerb), new ParsedVerb()
            {
                VerbName = "TestVerb",
                Options = new Dictionary<string, List<string>>()
                {
                    {"Name", new List<string> {"tarik"}},
                    {"age", new List<string> {"33"}},
                    {"ismale", new List<string> {"true"}}
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
                Options = new Dictionary<string, List<string>>()
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
                Options = new Dictionary<string, List<string>>()
                {
                    {"n", new List<string> {"tarik"}},
                    {"a", new List<string> {"33"}},
                    {"m", new List<string> {"true"}}
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
                    Options = new Dictionary<string, List<string>>
                    {
                        {"Money", new List<string> {"12.55"}}
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
                Options = new Dictionary<string, List<string>>()
                {
                    {"fn", new List<string> {"tarik"}},
                    {"a", new List<string> {"33"}},
                    {"im", new List<string> {"true"}}
                }
            });
            Assert.NotNull(optionsObject);
            Assert.Equal("tarik", optionsObject.Name);
            Assert.Equal(33, optionsObject.Age);
            Assert.True(optionsObject.Male);
        }
    }
}