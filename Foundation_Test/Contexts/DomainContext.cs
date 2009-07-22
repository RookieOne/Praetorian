using Foundation.DDD;
using Foundation.DDD.Interfaces;
using Foundation.Events;
using Foundation.Events.Interfaces;
using Foundation.Messages;
using Foundation.Messages.Interfaces;
using Foundation_Test.Mocks;
using Microsoft.Practices.Unity;
using UnityFoundation;

namespace Foundation_Test.Contexts
{
    public abstract class DomainContext : ContextSpecification
    {
        protected IUnityContainer _container;
        protected UnityEventHandlerResolver<IEntity> _eventHandlerResolver;
        protected MockEventRepository _eventRepository;
        protected UnityMessageConsumerRepository _messageConsumerRepository;
        protected MockMessageLog _messageLog;

        protected override void Given()
        {
            base.Given();

            _container = new UnityContainer();
            _container.RegisterInstance(_container);

            _eventRepository = new MockEventRepository();
            _container.RegisterInstance<IEventRepository>(_eventRepository);

            _messageLog = new MockMessageLog();
            _container.RegisterInstance<IMessageLog>(_messageLog);

            _messageConsumerRepository = new UnityMessageConsumerRepository();
            _container.RegisterInstance<IMessageConsumerRepository>(_messageConsumerRepository);

            _eventHandlerResolver = new UnityEventHandlerResolver<IEntity>(_container);
            _container.RegisterInstance<IEventHandlerResolver>(_eventHandlerResolver);

            var domainRepository = _container.Resolve<DomainRepository>();
            _container.RegisterInstance<IDomainRepository>(domainRepository);

            EventProcessor.Clear();
            EventProcessor.Setup(_container);

            MessageBroker.Setup(_container);
        }
    }
}