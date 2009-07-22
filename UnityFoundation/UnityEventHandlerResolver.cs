using System;
using System.Reflection;
using Foundation.Events;
using Foundation.Events.Interfaces;
using Microsoft.Practices.Unity;

namespace UnityFoundation
{
    public class UnityEventHandlerResolver<T> : IEventHandlerResolver
    {
        private readonly IUnityContainer _container;
        private readonly UnityEventRegisterer<T> _registerer;

        public UnityEventHandlerResolver(IUnityContainer container)
        {
            _container = container.CreateChildContainer();
            _registerer = new UnityEventRegisterer<T>(_container);
        }        

        public void RegisterAssemby(Assembly assembly)
        {
            _registerer.Register(assembly);
        }

        public object GetEventHandlerFor(Type type)
        {
            Type genericType = typeof(IHandle<>);

            Type consumerType = genericType.MakeGenericType(new[] { type });

            return _container.Resolve(consumerType, type.Name);
        }

        public object GetEventHandler<E>(E domainEvent)
        {
            Type genericType = typeof (IHandle<>);

            Type consumerType = genericType.MakeGenericType(new[] {domainEvent.GetType()});

            return _container.Resolve(consumerType, domainEvent.GetType().Name);
        }
    }
}