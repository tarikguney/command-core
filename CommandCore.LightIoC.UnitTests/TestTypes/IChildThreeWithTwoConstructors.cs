namespace CommandCore.LightIoC.UnitTests.TestTypes
{
    public interface IChildThreeWithTwoConstructors
    {
    }

    public class ChildThreeWithTwoConstructors : IChildThreeWithTwoConstructors
    {
        public ChildThreeWithTwoConstructors(IChildOne childOne)
        {
        }

        public ChildThreeWithTwoConstructors(IChildTwo childTwo)
        {
        }
    }
}