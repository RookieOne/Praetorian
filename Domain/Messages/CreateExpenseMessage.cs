using System;

namespace Domain.Messages
{
    public class CreateExpenseMessage : IDomainMessage
    {
        public Guid Id { get; protected set; }
        public string Title { get; protected set; }

        public CreateExpenseMessage(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }
    }
}