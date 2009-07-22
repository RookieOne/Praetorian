using System;
using Foundation.Builders;
using Foundation.Messages;

namespace ExpenseShared.Messages
{
    public class CreateExpenseMessage : AggregateMessage
    {        
        public string Title { get; set; }

        internal CreateExpenseMessage()
        {
            AggregateId = Guid.NewGuid();
        }
    }

    public static partial class ExpenseMessageBuilder
    {
        public static CreateExpenseBuilder CreateExpense(this MessageBuilder builder)
        {
            return new CreateExpenseBuilder();
        }

        public class CreateExpenseBuilder
        {
            private CreateExpenseMessage _message;

            public CreateExpenseBuilder()
            {
                _message = new CreateExpenseMessage();
            }

            public CreateExpenseBuilder WithTitle(string title)
            {
                _message.Title = title;
                return this;
            }

            public CreateExpenseMessage Send()
            {
                MessageBroker.Send(_message);
                return _message;
            }
        }
    }
}