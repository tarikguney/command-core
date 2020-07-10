using System;
using System.Collections.Generic;
using System.Reflection;
using CommandCore.Library.Interfaces;
using CommandCore.Library.UnitTests.TestTypes;
using Moq;
using Xunit;

namespace CommandCore.Library.UnitTests
{
    public class VerbTypeFinderTests
    {
        private readonly Mock<IEntryAssemblyProvider> _assemblyProviderMock;

        public VerbTypeFinderTests()
        {
            _assemblyProviderMock = new Mock<IEntryAssemblyProvider>();
        }
        
        [Fact]
        public void When_Assembly_Passed_Returns_All_Types()
        {
            var assemblyMock = new Mock<Assembly>();
            assemblyMock.Setup(a => a.GetTypes()).Returns(new[] {typeof(TestVerb)});
            _assemblyProviderMock.Setup(a => a.GetEntryAssembly())
                .Returns(assemblyMock.Object);
            var typeFinder = new VerbTypeFinder(_assemblyProviderMock.Object);
            var actualTypes = typeFinder.FindAll();
            Assert.NotNull(actualTypes);
            Assert.Equal(new List<Type> {typeof(TestVerb)}, actualTypes);
        }
        
        [Fact]
        public void When_Assembly_Passed_Finding_By_Name_Returns_Type_Using_Class_Name()
        {
            var assemblyMock = new Mock<Assembly>();
            assemblyMock.Setup(a => a.GetTypes()).Returns(new[] {typeof(TestVerb)});
            _assemblyProviderMock.Setup(a => a.GetEntryAssembly())
                .Returns(assemblyMock.Object);
            var typeFinder = new VerbTypeFinder(_assemblyProviderMock.Object);
            var actualType = typeFinder.FindByName("TestVerb");
            Assert.NotNull(actualType);
            Assert.Equal(typeof(TestVerb), actualType);
        }
        
        [Fact]
        public void When_Assembly_Passed_Finding_By_Name_Returns_Type_Using_Attribute_Name()
        {
            var assemblyMock = new Mock<Assembly>();
            assemblyMock.Setup(a => a.GetTypes()).Returns(new[] {typeof(TestVerbWithAttribute)});
            _assemblyProviderMock.Setup(a => a.GetEntryAssembly())
                .Returns(assemblyMock.Object);
            var typeFinder = new VerbTypeFinder(_assemblyProviderMock.Object);
            var actualType = typeFinder.FindByName("test");
            Assert.NotNull(actualType);
            Assert.Equal(typeof(TestVerbWithAttribute), actualType);
        }
        
        [Theory]
        [InlineData((string) null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_Null_Empty_VerbName_Passed_Throw_Invalid_Operations_Exception(string verbName)
        {
            var assemblyMock = new Mock<Assembly>();
            assemblyMock.Setup(a => a.GetTypes()).Returns(new[] {typeof(TestVerbWithAttribute)});
            _assemblyProviderMock.Setup(a => a.GetEntryAssembly())
                .Returns(assemblyMock.Object);
            var typeFinder = new VerbTypeFinder(_assemblyProviderMock.Object);
            Assert.Throws<InvalidOperationException>(()=>typeFinder.FindByName(verbName));
        }
    }
}