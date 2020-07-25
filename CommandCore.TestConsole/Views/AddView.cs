using System.Linq;
using CommandCore.Library.PublicBase;
using CommandCore.TestConsole.Options;

namespace CommandCore.TestConsole.Views
{
    public class AddView : VerbViewBase
    {
        private readonly AddOptions _options;
        private readonly IOutputWriter _writer;

        public AddView(AddOptions options, IOutputWriter writer)
        {
            _options = options;
            _writer = writer;
        }

        public override void RenderResponse()
        {
            _writer.Write(
                $"FirstName: {_options!.FirstName}\n" +
                $"Last Name: {_options!.LastName}\n" +
                $"Has License: {_options!.HasLicense}\n" +
                $"Age: {_options.Age}\n" +
                $"Ids: {_options.Ids.Select(a => a.ToString()).Aggregate((a, b) => a + "," + b)}");
        }
    }
}