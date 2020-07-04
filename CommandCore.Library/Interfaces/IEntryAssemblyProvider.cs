using System.Reflection;

namespace CommandCore.Library.Interfaces
{
    internal interface IEntryAssemblyProvider
    {
        Assembly GetEntryAssembly();
    }
}