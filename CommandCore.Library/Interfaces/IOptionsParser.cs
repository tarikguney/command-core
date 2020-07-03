using System;
using CommandCore.Library.PublicBase;

namespace CommandCore.Library.Interfaces
{
    public interface IOptionsParser
    {
        VerbOptionsBase CreatePopulatedOptionsObject(Type verbType, ParsedVerb parsedVerb);
    }
}