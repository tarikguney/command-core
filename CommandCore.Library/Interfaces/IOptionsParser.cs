using System;

namespace CommandCore.Library.Interfaces
{
    public interface IOptionsParser
    {
        Type GetAssociatedOptionsType(Type verbType);

        VerbOptionsBase CreatePopulatedOptionsObject(Type optionsType, ParsedVerb parsedVerb);
    }
}