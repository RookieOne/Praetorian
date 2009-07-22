using System;
using Foundation.Events;

namespace ExpenseShared.Events
{
    public class ExpenseCreatedEvent : IAggregateEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }

        public static Builder New()
        {
            return new Builder();
        }

        public class Builder
        {
            private ExpenseCreatedEvent _event;

            public Builder()
            {
                _event = new ExpenseCreatedEvent{ EventDate = DateTime.Today};                
            }

            public Builder For(Guid id)
            {
                _event.AggregateId = id;
                return this;
            }

            public Builder TitleSetTo(string title)
            {
                _event.Title = title;
                return this;
            }

            public ExpenseCreatedEvent Publish()
            {
                EventProcessor.Publish(_event);
                return _event;
            }

            public ExpenseCreatedEvent Create()
            {
                return _event;
            }
        }
    }
}