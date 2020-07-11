using CommandCore.Library.PublicBase;

namespace CommandCore.Library.UnitTests.TestTypes
{
    internal class TestVerb : VerbBase<TestOptions>
    {
        public override VerbViewBase Run()
        {
            return new TestView();
        }
    }
}