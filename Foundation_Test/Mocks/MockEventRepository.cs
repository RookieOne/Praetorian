using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.Events;

namespace Foundation_Test.Mocks
{
    public class MockEventRepository : IEventRepository
    {
        public List<IDomainEvent> EventLog { get; set; }
        public List<IAggregateEvent> AggregateEventLog { get; set; }

        public MockEventRepository()
        {
            EventLog = new List<IDomainEvent>();
            AggregateEventLog = new List<IAggregateEvent>();
        }

        public IDomainEvent Record(IDomainEvent domainEvent)
        {
            EventLog.Add(domainEvent);

            var aggregateEvent = domainEvent as IAggregateEvent;

            if (aggregateEvent != null)
                AggregateEventLog.Add(aggregateEvent);

            domainEvent.Id = Guid.NewGuid();
            return domainEvent;
        }

        public IEnumerable<IAggregateEvent> GetAggregateEvents(Guid id)
        {
            return AggregateEventLog.Where(e => e.AggregateId == id);
        }

        public int EventCount<T>()
        {
            return EventLog.Where(e => e.GetType() == typeof(T)).Count();
        }

        public T GetEvent<T>()
        {
            return (T) EventLog.FirstOrDefault(e => e.GetType() == typeof(T));
        }
    }
}