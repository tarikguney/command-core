using System.Reflection;
using CommandCore.Library.Interfaces;

namespace CommandCore.Library
{
    public class BasicEntryAssemblyProvider : IEntryAssemblyProvider
    {
        public Assembly GetEntryAssembly()
        {
            return Assembly.GetEntryAssembly()!;
        }
    }
}