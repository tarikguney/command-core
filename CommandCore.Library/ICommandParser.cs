namespace CommandCore.Library
{
    public interface ICommandParser
    {
        ParsedVerb ParseCommand(string[] arguments);
    }
}