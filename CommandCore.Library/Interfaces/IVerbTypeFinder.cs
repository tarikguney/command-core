using System;

namespace CommandCore.Library.Interfaces
{
    internal interface IVerbTypeFinder
    {
        Type? FindVerbTypeInExecutingAssembly(string verbName);
    }
}