using System;
using Domain.Events;
using Domain.Messages;

namespace Domain.DomainObjects
{
    public class Expense : IConsumer<CreateExpenseMessage>,
                           IConsumer<ApproveExpenseMessage>
    {
        private DateTime _approvalDate;
        private string _title;

        public void Consume(CreateExpenseMessage message)
        {
            _title = message.Title;

            var domainEvent = new ExpenseCreatedEvent(_title);
            EventProcessor.Publish(domainEvent);
        }

        public void Consume(ApproveExpenseMessage message)
        {
            _approvalDate = message.Date;
        }
    }
}