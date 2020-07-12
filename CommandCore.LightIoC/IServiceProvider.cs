using System;

namespace CommandCore.LightIoC
{
    public interface IServiceProvider
    {
        void Register<S, T>();

        void Register(Type service, Type implementation);
        
        void Register(Type service, object implementation);
        
        T Resolve<T>();

        object Resolve(Type serviceType);
    }
}