using System;

namespace CommandCore.Library.Interfaces
{
    public interface IVerbFinder
    {
        Type? FindVerbTypeInExecutingAssembly(string verbName);
    }
}