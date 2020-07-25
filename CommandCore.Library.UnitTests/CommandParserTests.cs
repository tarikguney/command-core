using System.Collections.Generic;
using Xunit;

namespace CommandCore.Library.UnitTests
{
    public class CommandParserTests
    {
        [Fact]
        public void When_Passed_A_Simple_Command_They_Are_Parsed_Properly()
        {
            var commandParser = new CommandParser();
            var arguments = new[]
            {
                "add",
                "--firstname", "tarik",
                "--lastname", "guney",
                "--zipcode", "55555"
            };

            var parsedVerb = commandParser.ParseCommand(arguments);

            Assert.NotNull(parsedVerb);
            Assert.Equal("add", parsedVerb.VerbName);
            Assert.Equal(new Dictionary<string, List<string>>
            {
                {"firstname", new List<string> {"tarik"}},
                {"lastname", new List<string> {"guney"}},
                {"zipcode", new List<string> {"55555"}}
            }, parsedVerb.Options);
        }

        [Fact]
        public void When_Passed_No_Verb_Default_Verb_Is_Used()
        {
            var commandParser = new CommandParser();
            var arguments = new[]
            {
                "--firstname", "tarik",
                "--lastname", "guney"
            };
            var parsedVerb = commandParser.ParseCommand(arguments);
            Assert.NotEmpty(parsedVerb.VerbName);
            Assert.Equal("default", parsedVerb.VerbName);
            Assert.Equal(new Dictionary<string, List<string>>
            {
                {"firstname", new List<string> {"tarik"}},
                {"lastname", new List<string> {"guney"}},
            }, parsedVerb.Options);
        }

        [Fact]
        public void When_No_Arguments_Passed_Only_Default_Verb_Returned()
        {
            var commandParser = new CommandParser();
            var parsedVerb = commandParser.ParseCommand(new string[0]);
            Assert.Equal("default", parsedVerb.VerbName);
            Assert.Null(parsedVerb.Options);
        }

        [Fact]
        public void When_Passed_Null_Arguments_Default_Verb_Selected()
        {
            var commandParser = new CommandParser();
            var parsedVerb = commandParser.ParseCommand(null);
            Assert.Equal("default", parsedVerb.VerbName);
            Assert.Null(parsedVerb.Options);
        }

        [Fact]
        public void When_Passed_Alias_Parsed_Properly()
        {
            var commandParser = new CommandParser();
            var arguments = new[]
            {
                "-t", "test",
                "--name", "tarik",
                "-g", "guney"
            };
            var parsedVerb = commandParser.ParseCommand(arguments);
            Assert.Equal("default", parsedVerb.VerbName);
            Assert.Equal(new Dictionary<string, List<string>>()
            {
                {"t", new List<string> {"test"}},
                {"name", new List<string> {"tarik"}},
                {"g", new List<string> {"guney"}}
            }, parsedVerb.Options);
        }

        [Fact]
        public void When_Alias_And_Full_Verbs_Randomized_In_Order_They_Are_Parsed_Properly()
        {
            var commandParser = new CommandParser();
            var arguments = new[]
            {
                "-t", "test",
                "--name", "tarik",
                "-g", "guney",
                "-hello", "world"
            };

            var parsedVerb = commandParser.ParseCommand(arguments);
            Assert.Equal("default", parsedVerb.VerbName);
            Assert.Equal(new Dictionary<string, List<string>>()
            {
                {"t", new List<string> {"test"}},
                {"name", new List<string> {"tarik"}},
                {"g", new List<string> {"guney"}},
                {"hello", new List<string> {"world"}}
            }, parsedVerb.Options);
        }
    }
}