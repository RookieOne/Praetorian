using System;

namespace Domain.Messages
{
    public class ApproveExpenseMessage : IDomainMessage
    {
        public Guid Id { get; protected set; }
        public DateTime Date { get; protected set; }

        public ApproveExpenseMessage(Guid id, DateTime date)
        {
            Id = id;
            Date = date;
        }
    }
}