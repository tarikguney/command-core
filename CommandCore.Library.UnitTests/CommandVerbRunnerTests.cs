using System;
using CommandCore.Library.Interfaces;
using Moq;
using Xunit;

namespace CommandCore.Library.UnitTests
{
    public class CommandVerbRunnerTests
    {
        [Fact]
        public void If_There_Is_No_Verb_Type_Then_Invalid_Operation_Exception_Is_Thrown()
        {
            var commandParseMock = new Mock<ICommandParser>();
            var verbTypeFinder = new Mock<IVerbTypeFinder>();
            var optionsParser = new Mock<IOptionsParser>();
            var helpGeneratorMock = new Mock<IHelpGenerator>();
            
            commandParseMock.Setup(a => a.ParseCommand(It.IsAny<string[]>()))
                .Returns(new ParsedVerb() {VerbName = "default"});
            verbTypeFinder.Setup(a => a.FindByName(It.IsAny<string>())).Returns((Type?) null);
            
            var runner = new CommandCoreVerbRunner(commandParseMock.Object, verbTypeFinder.Object, optionsParser.Object,
                helpGeneratorMock.Object);

            Assert.Throws<InvalidOperationException>(() => runner.Run(new string[0]));
        }
    }
}