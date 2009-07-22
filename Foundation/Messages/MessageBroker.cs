using System;
using Foundation.DDD.Interfaces;
using Foundation.Messages.Interfaces;
using Microsoft.Practices.Unity;

namespace Foundation.Messages
{
    public class MessageBroker
    {
        private static MessageBroker _messageBroker;

        [Dependency]
        public IDomainRepository DomainRepository { get; set; }

        [Dependency]
        public IMessageLog Logger { get; set; }

        [Dependency]
        public IMessageConsumerRepository MessageConsumerRepository { get; set; }

        public static void Setup(IUnityContainer container)
        {
            _messageBroker = container.Resolve<MessageBroker>();
        }

        public static void Send<T>(T message)
        {
            _messageBroker.Handle(message);
        }

        public void Handle<T>(T message)
        {
            var domainMessage = message as IDomainMessage;
            if (domainMessage != null)
            {
                domainMessage.DateRecieved = DateTime.Today;
                domainMessage.DateSent = DateTime.Today;
                Logger.Log(domainMessage);
            }

            IConsume<T> consumer = null;

            var aggregateMessage = message as IAggregateMessage;

            if (aggregateMessage != null)
            {
                consumer = DomainRepository.Get(aggregateMessage.AggregateId) as IConsume<T>;
            }

            if (consumer == null)            
            {
                consumer = MessageConsumerRepository.GetConsumer(message);
            }

            if (consumer != null)
                consumer.Consume(message);
        }
    }
}