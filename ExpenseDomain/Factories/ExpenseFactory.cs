using ExpenseDomain.Aggregates;
using ExpenseShared.Messages;
using Foundation.Messages.Interfaces;

namespace ExpenseDomain.Factories
{
    public class ExpenseFactory : IConsume<CreateExpenseMessage>
    {
        public void Consume(CreateExpenseMessage message)
        {
            new Expense().Create(message);
        }
    }
}