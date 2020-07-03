namespace CommandCore.LightIoC
{
    public interface IServiceProvider
    {
        void Register<S, T>() where T : S;
        T Resolve<T>();
    }
}