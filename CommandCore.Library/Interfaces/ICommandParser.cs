namespace CommandCore.Library.Interfaces
{
    internal interface ICommandParser
    {
        ParsedVerb ParseCommand(string[] arguments);
    }
}