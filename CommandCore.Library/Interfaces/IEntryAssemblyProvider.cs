using System.Reflection;

namespace CommandCore.Library.Interfaces
{
    public interface IEntryAssemblyProvider
    {
        Assembly GetEntryAssembly();
    }
}