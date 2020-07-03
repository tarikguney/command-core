using System.Reflection;
using CommandCore.Library.Interfaces;

namespace CommandCore.Library
{
    internal class BasicEntryAssemblyProvider : IEntryAssemblyProvider
    {
        public Assembly GetEntryAssembly()
        {
            return Assembly.GetEntryAssembly()!;
        }
    }
}