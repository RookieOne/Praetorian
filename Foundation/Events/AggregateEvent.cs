using System;

namespace Foundation.Events
{
    public abstract class AggregateEvent : DomainEvent, IAggregateEvent
    {
        public virtual Guid AggregateId { get; set; }
    }
}