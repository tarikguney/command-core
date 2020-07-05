using System.Text;

namespace CommandCore.Library
{
    internal interface IHelpGenerator
    {
        StringBuilder Build();
    }
}