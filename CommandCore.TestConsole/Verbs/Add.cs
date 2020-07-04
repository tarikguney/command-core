using CommandCore.Library.Attributes;
using CommandCore.Library.PublicBase;
using CommandCore.TestConsole.Options;
using CommandCore.TestConsole.Views;

namespace CommandCore.TestConsole.Verbs
{
    [VerbName("add")]
    public class Add : VerbBase<AddOptions>
    {
        public override VerbViewBase Run()
        {
            return new AddView(Options);
        }
    }
}