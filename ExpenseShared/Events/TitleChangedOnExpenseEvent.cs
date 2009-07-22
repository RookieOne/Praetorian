using System;
using Foundation.Builders;
using Foundation.Events;

namespace ExpenseShared.Events
{
    public class TitleChangedOnExpenseEvent : AggregateEvent
    {
        public string Title { get; set; }

        internal TitleChangedOnExpenseEvent(Guid id)
        {
            AggregateId = id;
        }
    }

    public static partial class ExpenseEventBuilder
    {
        public static TitleChangedOnExpenseBuilder TitleChangedOn(this EventBuilder builder, Guid id)
        {
            return new TitleChangedOnExpenseBuilder(id);
        }

        public class TitleChangedOnExpenseBuilder
        {
            private readonly TitleChangedOnExpenseEvent _event;

            public TitleChangedOnExpenseBuilder(Guid id)
            {
                _event = new TitleChangedOnExpenseEvent(id);
            }

            public TitleChangedOnExpenseBuilder To(string title)
            {
                _event.Title = title;
                return this;
            }

            public TitleChangedOnExpenseEvent Publish()
            {
                EventProcessor.Publish(_event);
                return _event;
            }
        }
    }
}