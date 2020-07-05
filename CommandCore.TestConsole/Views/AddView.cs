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
            Console.WriteLine(
                $"FirstName: {_options!.FirstName}\n" +
                $"Last Name: {_options!.LastName}\n" +
                $"Has License: {_options!.HasLicense}\n" +
                $"Age: {_options.Age}");
        }
    }
}