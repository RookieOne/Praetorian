using System.Reflection;
using ExpenseDomain.Repositories;
using Foundation.DDD;
using Foundation.DDD.Interfaces;
using Foundation.Events;
using Foundation_Test.Contexts;
using Foundation_Test.Mocks;
using UnityFoundation;

namespace ExpenseDomain_Test.Contexts
{
    public class WriteRepositoriesContext : DomainContext
    {
        protected MockWriteRepositoryStrategy _database;
        protected UnityEventHandlerResolver<IWriteRepository> _writeEventHandlerRepository;
        protected WriteRepositoryEventSubscriber _writeRepositoryEventSubscriber;

        protected override void Given()
        {
            base.Given();

            _database = new MockWriteRepositoryStrategy();
            _container.RegisterInstance<IWriteRepositoryStrategy>(_database);

            _writeEventHandlerRepository = new UnityEventHandlerResolver<IWriteRepository>(_container);
            _writeEventHandlerRepository.RegisterAssemby(Assembly.GetAssembly(typeof (ExpenseWriteRepository)));

            _writeRepositoryEventSubscriber = new WriteRepositoryEventSubscriber(_writeEventHandlerRepository);

            EventProcessor.SubscribeToAll(_writeRepositoryEventSubscriber);
        }
    }
}