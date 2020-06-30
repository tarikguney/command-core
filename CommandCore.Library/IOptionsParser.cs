using System;

namespace CommandCore.Library
{
    public interface IOptionsParser
    {
        Type GetAssociatedOptionsType(Type verbType);

        VerbOptions CreatePopulatedOptionsObject(Type optionsType, ParsedVerb parsedVerb);
    }
}