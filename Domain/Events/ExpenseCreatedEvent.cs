using System;

namespace Domain.Events
{
    public class ExpenseCreatedEvent : IDomainEvent
    {
        public Guid Id { get; protected set; }
        public string Title { get; protected set; }
        public DateTime EventDate { get; protected set; }

        public ExpenseCreatedEvent(string title)
        {
            Title = title;
            EventDate = DateTime.Today;
        }
    }
}