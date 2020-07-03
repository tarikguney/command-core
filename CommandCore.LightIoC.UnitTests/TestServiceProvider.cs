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
    }
}