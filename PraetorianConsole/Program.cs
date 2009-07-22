using System;
using System.Reflection;
using ExpenseDomain.Aggregates;
using ExpenseDomain.Repositories;
using ExpenseShared.Messages;
using ExpenseShared.Repositories;
using Foundation.Builders;
using Foundation.DDD;
using Foundation.DDD.Interfaces;
using Foundation.Events;
using Foundation.Events.Interfaces;
using Foundation.Messages;
using Foundation.Messages.Interfaces;
using Microsoft.Practices.Unity;
using NHibDataFoundation.Configs;
using NHibDataFoundation.Events;
using NHibDataFoundation.Messages;
using NHibDataFoundation.Repositories;
using NHibExpense.Mappings;
using UnityFoundation;

namespace PraetorianConsole
{
    internal class Program
    {
        private static UnityContainer _container;
        private static Guid _id;

        private static void Main(string[] args)
        {
            Console.WriteLine("Start");

            Setup();

            New.Message()
                .CreateExpense()
                .WithTitle("Console Expense")
                .Send();

            PrintExpenses();
            Console.ReadLine();

            New.Message()
                .ChangeTitleOn(_id)
                .To("New Title")
                .Send();

            PrintExpenses();
            Console.ReadLine();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void PrintExpenses()
        {
            var expenses = _container.Resolve<ExpenseReadRepository>().GetAllExpenses();

            foreach(var expense in expenses)
            {
                Console.WriteLine(expense.Title);
                _id = expense.Id;
            }
            Console.WriteLine("Done Printing...");
        }



        private static void Setup()
        {
            var container = new UnityContainer();
            container.RegisterInstance(container);

            NHibConfig config = NHibConfig.Create()
                .DatabaseNameIs("Praetorian")
                .ServerIs("Lubu\\SQLEXPRESS2")
                .RegisterMappings(Assembly.GetAssembly(typeof (NHibEventRepository)))
                .RegisterMappings(Assembly.GetAssembly(typeof (ExpenseDtoMapping)))
                .Build();

            container.RegisterInstance<IEventRepository>(new NHibEventRepository(config));

            container.RegisterInstance<IMessageLog>(new NHibMessageLog(config));

            var repository = new UnityMessageConsumerRepository();
            repository.RegisterAssemby(Assembly.GetAssembly(typeof (Expense)));
            container.RegisterInstance<IMessageConsumerRepository>(repository);

            container.RegisterInstance<IEventHandlerResolver>(new UnityEventHandlerResolver<IEntity>(container));

            container.RegisterInstance<IDomainRepository>(container.Resolve<DomainRepository>());

            container.RegisterInstance<IWriteRepositoryStrategy>(new NHibWriteRepositoryStrategy(config));
            container.RegisterInstance<IReadRepositoryStrategy>(new NHibReadRepositoryStrategy(config));

            var writeEventHandlerRepository = new UnityEventHandlerResolver<IWriteRepository>(container);
            writeEventHandlerRepository.RegisterAssemby(Assembly.GetAssembly(typeof (ExpenseWriteRepository)));

            var writeRepositoryEventSubscriber = new WriteRepositoryEventSubscriber(writeEventHandlerRepository);

            EventProcessor.SubscribeToAll(writeRepositoryEventSubscriber);

            EventProcessor.Setup(container);
            MessageBroker.Setup(container);

            _container = container;
        }
    }
}