using System;
using CommandCore.Library.PublicBase;
using CommandCore.TestConsole.Options;

namespace CommandCore.TestConsole.Views
{
    public class AddView : VerbViewBase
    {
        private readonly AddOptions _options;

        public AddView(AddOptions options)
        {
            _options = options;
        }
        
        public override void RenderResponse()
        {
            Console.WriteLine($"FirstName: {_options!.FirstName}\nLast Name: {_options!.LastName}\nEmployed: {_options!.Employed}");
        }
    }
}