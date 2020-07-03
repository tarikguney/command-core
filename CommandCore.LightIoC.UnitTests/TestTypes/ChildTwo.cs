namespace CommandCore.LightIoC.UnitTests.TestTypes
{
    public class ChildTwo : IChildTwo
    {
        public IGrandChildOne GrandChildOne { get; }

        public ChildTwo(IGrandChildOne grandChildOne)
        {
            GrandChildOne = grandChildOne;
        }
    }
}