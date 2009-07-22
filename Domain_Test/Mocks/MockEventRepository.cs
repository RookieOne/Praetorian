using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Events;

namespace Domain_Test.Mocks
{
    public class MockEventRepository : IEventRepository
    {
        public List<IDomainEvent> EventLog { get; set; }

        public MockEventRepository()
        {
            EventLog = new List<IDomainEvent>();
        }

        public void Record(IDomainEvent domainEvent)
        {
            EventLog.Add(domainEvent);
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid id)
        {
            return EventLog.Where(e => e.Id == id);
        }
    }
}