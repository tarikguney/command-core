using System;

namespace CommandCore.Library.Interfaces
{
    public interface IOptionsParser
    {
        Type GetAssociatedOptionsType(Type verbType);

        VerbOptions CreatePopulatedOptionsObject(Type optionsType, ParsedVerb parsedVerb);
    }
}