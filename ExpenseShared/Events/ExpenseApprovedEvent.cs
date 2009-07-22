using System;
using Foundation.Events;

namespace ExpenseShared.Events
{
    public class ExpenseApprovedEvent : AggregateEvent
    {
        public DateTime ApprovalDate { get; protected set; }

        public ExpenseApprovedEvent(Guid id, DateTime approvalDate)
        {
            AggregateId = id;
            ApprovalDate = approvalDate;
            EventDate = DateTime.Today;
        }
    }
}