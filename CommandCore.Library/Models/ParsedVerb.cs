using System.Collections.Generic;

public class ParsedVerb
{
    public string? VerbName { get; set; }

    // ToDo: Ideally the value should be anything. I don't know how I should design this right now. 
    // The reason is simple: Some arguments are flag attributes.
    public IReadOnlyDictionary<string, List<string>>? Options { get; set; }
}