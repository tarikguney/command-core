using System;
using System.Collections.Generic;

namespace CommandCore.Library.Interfaces
{
    internal interface IVerbTypeFinder
    {
        Type? FindByName(string verbName);
        IReadOnlyList<Type> FindAll();
    }
}