namespace CommandCore.Library.Interfaces
{
    public interface ICommandParser
    {
        ParsedVerb ParseCommand(string[] arguments);
    }
}