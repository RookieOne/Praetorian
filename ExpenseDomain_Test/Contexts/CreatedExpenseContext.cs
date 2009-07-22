using System;
using ExpenseShared.Events;

namespace ExpenseDomain_Test.Contexts
{
    public class CreatedExpenseContext : ExpenseDomainContext
    {
        protected Guid _expenseId;
        protected string _title;

        protected override void Given()
        {
            base.Given();

            _expenseId = Guid.NewGuid();
            _title = "Test";

            var createdEvent = ExpenseCreatedEvent.New()
                .For(_expenseId)
                .TitleSetTo(_title)
                .Create();

            _eventRepository.Record(createdEvent);
        }
    }
}