using System;
using Foundation.Messages.Interfaces;

namespace Foundation.Messages
{
    public class AggregateMessage : DomainMessage, IAggregateMessage
    {
        public Guid AggregateId { get; set; }
    }
}