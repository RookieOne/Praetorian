using System;
using Foundation.Builders;
using Foundation.Messages;

namespace ExpenseShared.Messages
{
    public class ChangeTitleMessage : AggregateMessage
    {        
        public string Title { get; set; }
    }

    public static partial class ExpenseMessageBuilder
    {
        public static ChangeTitleBuilder ChangeTitleOn(this MessageBuilder builder, Guid id)
        {
            return new ChangeTitleBuilder(id);
        }

        public class ChangeTitleBuilder
        {
            private readonly ChangeTitleMessage _message;

            public ChangeTitleBuilder(Guid id)
            {
                _message = new ChangeTitleMessage{ AggregateId = id};
            }

            public ChangeTitleBuilder To(string title)
            {
                _message.Title = title;
                return this;
            }

            public ChangeTitleMessage Send()
            {
                MessageBroker.Send(_message);
                return _message;
            }
        }
    }
}