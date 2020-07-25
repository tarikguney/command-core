using System;
using System.Collections.Generic;
using CommandCore.Library.Interfaces;
using CommandCore.Library.UnitTests.TestTypes;
using CommandCore.LightIoC;
using Moq;
using Xunit;

namespace CommandCore.Library.UnitTests
{
    public class CommandVerbRunnerTests
    {
        private readonly Mock<ICommandParser> _commandParseMock;
        private readonly Mock<IVerbTypeFinder> _verbTypeFinder;
        private readonly Mock<IOptionsParser> _optionsParser;
        private readonly Mock<IHelpGenerator> _helpGeneratorMock;
        private readonly LightIoC.IServiceProvider _serviceProviderMock;

        public CommandVerbRunnerTests()
        {
            _commandParseMock = new Mock<ICommandParser>();
            _verbTypeFinder = new Mock<IVerbTypeFinder>();
            _optionsParser = new Mock<IOptionsParser>();
            _helpGeneratorMock = new Mock<IHelpGenerator>();
            _serviceProviderMock = new BasicServiceProvider();
        }

        [Fact]
        public void If_There_Is_No_Verb_Type_Then_Invalid_Operation_Exception_Is_Thrown()
        {
            _commandParseMock.Setup(a => a.ParseCommand(It.IsAny<string[]>()))
                .Returns(new ParsedVerb() {VerbName = "default"});
            _verbTypeFinder.Setup(a => a.FindByName(It.IsAny<string>())).Returns((Type?) null);

            var runner = new CommandCoreVerbRunner(_commandParseMock.Object, _verbTypeFinder.Object,
                _optionsParser.Object,
                _helpGeneratorMock.Object, _serviceProviderMock);

            Assert.Throws<InvalidOperationException>(() => runner.Run(new string[0]));
        }

        [Fact]
        public void If_Help_Flag_Passed_Help_Generator_Invoked()
        {
            var runner = new CommandCoreVerbRunner(_commandParseMock.Object, _verbTypeFinder.Object,
                _optionsParser.Object,
                _helpGeneratorMock.Object, _serviceProviderMock);

            runner.Run(new string[] {"--help"});
            // Normally I don't like verifying the internal calls like this, but for this is an exception since I need
            // to know if the generator is called. Perhaps, in the future, I can check if something is returned.
            _helpGeneratorMock.Verify(a => a.Build());
        }

        [Fact]
        public void If_Every_Thing_Passed_Properly_Zero_Return_Code_Returns()
        {
            var args = new[] {"--name", "tarik"};
            var testVerbInfo = new ParsedVerb()
            {
                VerbName = "default", Options = new Dictionary<string, List<string>>()
                {
                    {"name", new List<string> {"tarik"}}
                }
            };
            _commandParseMock.Setup(a => a.ParseCommand(It.IsAny<string[]>()))
                .Returns(testVerbInfo);

            _verbTypeFinder.Setup(a => a.FindByName(It.IsAny<string>()))
                .Returns(typeof(TestVerb));

            _optionsParser.Setup(a => a.CreatePopulatedOptionsObject(typeof(TestVerb), testVerbInfo))
                .Returns(new TestOptions()
                {
                    Name = "tarik"
                });

            var runner = new CommandCoreVerbRunner(_commandParseMock.Object, _verbTypeFinder.Object,
                _optionsParser.Object,
                _helpGeneratorMock.Object, _serviceProviderMock);

            Assert.Equal(0, runner.Run(args));
        }
    }
}