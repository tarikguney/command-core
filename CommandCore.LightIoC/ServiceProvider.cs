using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CommandCore.LightIoC
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly ConcurrentDictionary<Type, Type> _typeRegistry = new ConcurrentDictionary<Type, Type>();

        public void Register<S, T>() where T : S
        {
            _typeRegistry[typeof(S)] = typeof(T);
        }

        public T Resolve<T>()
        {
            return (T) CreateInstance(typeof(T));
        }

        private object CreateInstance(Type type)
        {
            var registeredType = _typeRegistry[type];
            var constructors = registeredType.GetConstructors();
            if (constructors.Length > 0)
            {
                throw new Exception($"There must be only one constructor method defined for type {type.FullName}");
            }

            if (constructors.Length == 0)
            {
                throw new Exception($"There could not be found any constructor method for {type.FullName}");
            }

            var injectedTypes = constructors[0].GetParameters().Select(a => a.ParameterType);

            if (!injectedTypes.Any())
            {
                return Activator.CreateInstance(registeredType);
            }

            var instances = new List<object>();
            foreach (var injectedType in injectedTypes)
            {
                instances.Add(CreateInstance(injectedType));
            }

            return Activator.CreateInstance(registeredType, instances);
        }
    }
}