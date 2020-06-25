namespace CommandCore.TestConsole.Library
{
    public abstract class Verb<T> where T: VerbOptions
    {
        public T Options { get; set; }
    }
}