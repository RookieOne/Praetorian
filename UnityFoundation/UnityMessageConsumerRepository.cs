using System;
using System.Reflection;
using Foundation.Messages.Interfaces;
using Microsoft.Practices.Unity;

namespace UnityFoundation
{
    public class UnityMessageConsumerRepository : IMessageConsumerRepository
    {
        private readonly IUnityContainer _container;
        private readonly UnityMessageRegisterer _registerer;

        public UnityMessageConsumerRepository()
        {
            _container = new UnityContainer();
            _registerer = new UnityMessageRegisterer(_container);
        }

        public void RegisterAssemby(Assembly assembly)
        {
            _registerer.Register(assembly);
        }

        public IConsume<T> GetConsumer<T>(T message)
        {
            Type genericType = typeof (IConsume<>);

            Type consumerType = genericType.MakeGenericType(new[] {message.GetType()});

            return _container.Resolve(consumerType, message.GetType().Name) as IConsume<T>;
        }
    }
}