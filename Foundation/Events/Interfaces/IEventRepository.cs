using System;
using System.Collections.Generic;

namespace Foundation.Events
{
    public interface IEventRepository
    {
        IDomainEvent Record(IDomainEvent domainEvent);
        IEnumerable<IAggregateEvent> GetAggregateEvents(Guid id);
    }
}