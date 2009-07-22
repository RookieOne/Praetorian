using System.Reflection;
using Domain.DomainObjects;
using Domain.Events;
using Domain.Foundation;
using Domain.Messages;
using Domain_Test.Mocks;
using Microsoft.Practices.Unity;

namespace Domain_Test.Contexts
{
    public abstract class DomainContext
    {
        protected MockEventRepository _eventRepository;
        protected MockMessageLogger _messageLogger;
        protected IUnityContainer _container;

        protected DomainContext()
        {
            _container = new UnityContainer();
            _container.RegisterInstance<IUnityContainer>(_container);

            var assembly = Assembly.GetAssembly(typeof (Expense));

            new UnityAssemblyLoader(assembly).Register(_container);
            new UnityMessageRegisterer(assembly).Register(_container);

            _eventRepository = new MockEventRepository();
            _container.RegisterInstance<IEventRepository>(_eventRepository);

            _messageLogger = new MockMessageLogger();
            _container.RegisterInstance<IMessageLogger>(_messageLogger);

            EventProcessor.Setup(_container);
            MessageProcessor.Setup(_container);

            When();
        }

        protected abstract void When();
    }
}