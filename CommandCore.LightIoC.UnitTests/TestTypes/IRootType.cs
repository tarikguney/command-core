namespace CommandCore.LightIoC.UnitTests.TestTypes
{
    public interface IRootType
    {
        IChildOne ChildOne { get; }
        IChildTwo ChildTwo { get; }
    }
}