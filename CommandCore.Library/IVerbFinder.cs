using System;

namespace CommandCore.Library
{
    public interface IVerbFinder
    {
        Type? FindVerbTypeInExecutingAssembly(string verbName);
    }
}