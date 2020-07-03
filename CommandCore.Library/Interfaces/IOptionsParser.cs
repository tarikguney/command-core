using System;

namespace CommandCore.Library.Interfaces
{
    public interface IOptionsParser
    {
        VerbOptionsBase CreatePopulatedOptionsObject(Type verbType, ParsedVerb parsedVerb);
    }
}