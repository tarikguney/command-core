using System;
using System.Collections.Generic;
using CommandCore.LightIoC.UnitTests.TestTypes;
using Xunit;

namespace CommandCore.LightIoC.UnitTests
{
    public class TestServiceProvider
    {
        [Fact]
        public void When_Registered_Resolve_Instantiates_Dependency_Tree()
        {
            var serviceProvider = new BasicServiceProvider();
            serviceProvider.Register<IRootType, RootType>();
            serviceProvider.Register<IChildOne, ChildOne>();
            serviceProvider.Register<IChildTwo, ChildTwo>();
            serviceProvider.Register<IGrandChildOne, GrandChildOne>();

            var instance = serviceProvider.Resolve<IRootType>();
            Assert.NotNull(instance);

            Assert.NotNull(instance.ChildOne);
            Assert.NotNull(instance.ChildTwo);
            
            Assert.NotNull(instance.ChildTwo.GrandChildOne);
        }

        [Fact]
        public void When_More_Constructors_Throws_Exception()
        {
            var serviceProvider = new BasicServiceProvider();
            serviceProvider.Register<IChildThreeWithTwoConstructors, ChildThreeWithTwoConstructors>();

            Assert.Throws<Exception>(() => serviceProvider.Resolve<IChildThreeWithTwoConstructors>());
        }

        [Fact]
        public void When_Not_Registered_Resolution_Throws_Key_Not_Found_Exception()
        {
            var serviceProvider = new BasicServiceProvider();
            serviceProvider.Register<IRootType, RootType>();
            serviceProvider.Register<IChildOne, ChildOne>();

            Assert.Throws<KeyNotFoundException>(() => serviceProvider.Resolve<IRootType>());
        }

        [Fact]
        public void When_Registering_Abstract_Service_Class_Throws_Invalid_Operations_Exception()
        {
            var serviceProvider = new BasicServiceProvider();
            Assert.Throws<InvalidOperationException>(() => serviceProvider.Register<IAnAbstractType, AnAbstractType>());
        }
    }
}