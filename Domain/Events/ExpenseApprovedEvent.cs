using System;

namespace Domain.Events
{
    public class ExpenseApprovedEvent : IDomainEvent
    {
        public DateTime ApprovalDate { get; protected set; }
        public DateTime EventDate { get; protected set; }
        public Guid Id { get; protected set; }

        public ExpenseApprovedEvent(DateTime approvalDate)
        {
            ApprovalDate = approvalDate;
            EventDate = DateTime.Today;
        }
    }
}