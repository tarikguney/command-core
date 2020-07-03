using CommandCore.Library.Interfaces;

namespace CommandCore.Library.PublicBase
{
    public abstract class VerbBase<T>: IVerbRunner where T: VerbOptionsBase
    {
        public T? Options { get; set; }
        
        public abstract void Run();
    }
}