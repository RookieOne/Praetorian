using System;
using Foundation.Builders;
using Foundation.Messages;

namespace ExpenseShared.Messages
{
    public class ApproveExpenseMessage : AggregateMessage
    {        
        public DateTime ApprovalDate { get; set; }

        internal ApproveExpenseMessage(Guid id)
        {
            AggregateId = id;
        }
    }

    public static partial class ExpenseMessageBuilder
    {
        public static ApproveExpenseBuilder ApproveExpense(this MessageBuilder builder, Guid id)
        {
            return new ApproveExpenseBuilder(id);
        }

        public class ApproveExpenseBuilder
        {
            private ApproveExpenseMessage _message;

            public ApproveExpenseBuilder(Guid id)
            {
                _message = new ApproveExpenseMessage(id);
            }

            public ApproveExpenseBuilder On(DateTime approvalDate)
            {
                _message.ApprovalDate = approvalDate;
                return this;
            }

            public void Send()
            {
                MessageBroker.Send(_message);
            }
        }
    }
}