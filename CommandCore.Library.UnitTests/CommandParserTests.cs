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
            Assert.Equal(new Dictionary<string, string>
            {
                {"firstname", "tarik"},
                {"lastname", "guney"},
                {"zipcode", "55555"}
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
            Assert.Equal(new Dictionary<string, string>
            {
                {"firstname", "tarik"},
                {"lastname", "guney"},
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
            var arguments = new string[]
            {
                "-t", "test",
                "--name", "tarik",
                "-g", "guney"
            };
            var parsedVerb = commandParser.ParseCommand(arguments);
            Assert.Equal("default", parsedVerb.VerbName);
            Assert.Equal(new Dictionary<string,string>()
            {
                {"t","test"},
                {"name","tarik"},
                {"g","guney"}
            }, parsedVerb.Options);
        }
    }
}