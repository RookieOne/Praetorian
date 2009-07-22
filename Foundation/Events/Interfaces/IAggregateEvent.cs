using System;

namespace Foundation.Events
{
    public interface IAggregateEvent : IDomainEvent
    {
        Guid AggregateId { get; }
    }
}