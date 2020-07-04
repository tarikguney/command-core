using System;
using CommandCore.Library.PublicBase;

namespace CommandCore.Library.Interfaces
{
    internal interface IOptionsParser
    {
        VerbOptionsBase CreatePopulatedOptionsObject(Type verbType, ParsedVerb parsedVerb);
    }
}