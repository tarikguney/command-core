using CommandCore.Library.Attributes;
using CommandCore.Library.PublicBase;

namespace CommandCore.Library.UnitTests.TestTypes
{
    [VerbName("test")]
    internal class TestVerbWithAttribute : VerbBase<TestOptions>
    {
        public override VerbViewBase Run()
        {
            throw new System.NotImplementedException();
        }
    }
}