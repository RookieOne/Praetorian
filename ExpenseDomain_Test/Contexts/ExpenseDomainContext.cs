using System.Reflection;
using ExpenseDomain.Aggregates;
using ExpenseDomain.Factories;
using Foundation_Test.Contexts;

namespace ExpenseDomain_Test.Contexts
{
    public class ExpenseDomainContext : DomainContext
    {
        protected override void Given()
        {
            base.Given();

            _container.RegisterInstance(new ExpenseFactory());

            var assembly = Assembly.GetAssembly(typeof (Expense));

            _messageConsumerRepository.RegisterAssemby(assembly);
            _eventHandlerResolver.RegisterAssemby(assembly);
        }
    }
}