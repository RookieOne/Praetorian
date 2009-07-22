using System;
using Foundation.Events;

namespace NHibDataFoundation.Events
{
    public class NHibAggregateEvent
    {
        public virtual Guid Id { get; set; }
        public virtual Guid EventId { get; set; }
        public virtual Guid AggregateId { get; set; }
    }
}