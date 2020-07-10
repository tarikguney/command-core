using System;
using System.Collections.Generic;
using CommandCore.Library.Interfaces;
using CommandCore.Library.UnitTests.TestTypes;
using Moq;
using Xunit;

namespace CommandCore.Library.UnitTests
{
    public class HelpGeneratorTests
    {
        [Fact]
        public void When_Types_Found_Help_Generated()
        {
            var typeFinderMock = new Mock<IVerbTypeFinder>();
            typeFinderMock.Setup(a => a.FindAll()).Returns(new List<Type>() {typeof(TestVerb)});
            var helpGenerator = new HelpGenerator(typeFinderMock.Object);
            var helpText = helpGenerator.Build();
            Assert.NotEmpty(helpText.ToString());
            Assert.True(helpText.ToString().Contains("TestVerb"));
        }
        
        [Fact]
        public void When_There_Is_No_Type_It_Prints_Warning()
        {
            var typeFinderMock = new Mock<IVerbTypeFinder>();
            typeFinderMock.Setup(a => a.FindAll()).Returns(new List<Type>());
            var helpGenerator = new HelpGenerator(typeFinderMock.Object);
            var helpText = helpGenerator.Build();
            Assert.NotEmpty(helpText.ToString());
            Assert.Equal("No help is found!", helpText.ToString());
        }
    }
}