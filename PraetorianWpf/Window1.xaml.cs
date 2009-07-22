using System.Reflection;
using System.Windows;
using ExpenseDomain.Aggregates;
using ExpenseDomain.Repositories;
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
using PraetorianWpf.ViewModels;
using UnityFoundation;

namespace PraetorianWpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            var container = new UnityContainer();
            container.RegisterInstance(container);

            NHibConfig config = NHibConfig.Create()
                .DatabaseNameIs("Praetorian")
                .ServerIs("localhost\\SQLEXPRESS2")
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

            content.Content = container.Resolve<ExpensesViewModel>();
        }
    }
}