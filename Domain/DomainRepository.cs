using System;
using Domain.Events;
using Domain.Messages;
using Microsoft.Practices.Unity;

namespace Domain
{
    public class DomainRepository
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        [Dependency]
        public IEventRepository EventRepository { get; set; }


        public object CreateObject(IDomainEvent domainEvent)
        {
            var genericType = typeof(IEventHandler<>);

            var consumerType = genericType.MakeGenericType(new Type[] { domainEvent.GetType() });

            return Container.Resolve(consumerType, domainEvent.GetType().Name);
        }

        public object GetObject(Guid id)
        {
            var domainEvents = EventRepository.GetEvents(id);

            object domainObject = null;

            foreach (var domainEvent in domainEvents)
            {
                if (domainObject == null)
                    domainObject = CreateObject(domainEvent);

                Process(domainEvent, domainObject);
            }

            return null;
        }

        public IConsumer<T> GetObject<T>(T message)
        {
            var domainMessage = message as IDomainMessage;

            var domainObject = GetObject(domainMessage.Id);

            if (domainObject == null)
            {
                var genericType = typeof(IConsumer<>);

                var consumerType = genericType.MakeGenericType(new Type[] { message.GetType() });

                domainObject = Container.Resolve(consumerType, message.GetType().Name);
            }

            return domainObject as IConsumer<T>;
        }

        private void Process<T>(T domainEvent, object domainObject)
        {
            var consumer = domainObject as IConsumer<T>;
            
            consumer.Consume(domainEvent);
        }
    }
}