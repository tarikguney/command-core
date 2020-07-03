using System;

namespace CommandCore.Library.Interfaces
{
    public interface IVerbTypeFinder
    {
        Type? FindVerbTypeInExecutingAssembly(string verbName);
    }
}