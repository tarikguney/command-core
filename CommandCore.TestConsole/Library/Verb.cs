namespace CommandCore.TestConsole.Library
{
    public abstract class Verb<T> where T: Options
    {
        public T Options { get; set; }
    }
}