using System;
using ExpenseShared.Events;
using ExpenseShared.Messages;
using Foundation.Builders;
using Foundation.DDD.Interfaces;
using Foundation.Events;
using Foundation.Messages.Interfaces;

namespace ExpenseDomain.Aggregates
{
    public class Expense : IEntity,
                           IConsume<ApproveExpenseMessage>,
                           IConsume<ChangeTitleMessage>,
                           IHandle<ExpenseCreatedEvent>,
                           IHandle<TitleChangedOnExpenseEvent>
    {
        public DateTime ApprovalDate { get; private set; }
        public Guid Id { get; private set; }
        public string Title { get; private set; }

        public void Create(CreateExpenseMessage message)
        {
            //Id = Guid.NewGuid();            
            Id = message.AggregateId;
            Title = message.Title;

            ExpenseCreatedEvent.New()
                .For(Id)
                .TitleSetTo(Title)
                .Publish();
        }

        public void Consume(ApproveExpenseMessage message)
        {
            ApprovalDate = message.ApprovalDate;

            EventProcessor.Publish(new ExpenseApprovedEvent(Id, ApprovalDate));
        }

        public void Handle(ExpenseCreatedEvent createdEvent)
        {
            Id = createdEvent.Id;
            Title = createdEvent.Title;
        }

        public void Consume(ChangeTitleMessage message)
        {
            Title = message.Title;

            New.Event()
                .TitleChangedOn(Id)
                .To(Title)
                .Publish();
        }

        public void Handle(TitleChangedOnExpenseEvent domainEvent)
        {
            Title = domainEvent.Title;
        }
    }
}