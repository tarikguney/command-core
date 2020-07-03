namespace CommandCore.LightIoC.UnitTests.TestTypes
{
    public class RootType : IRootType
    {
        public IChildOne ChildOne { get; }
        public IChildTwo ChildTwo { get; }

        public RootType(IChildOne childOne, IChildTwo childTwo)
        {
            ChildOne = childOne;
            ChildTwo = childTwo;
        }
    }
}