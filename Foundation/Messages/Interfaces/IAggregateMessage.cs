using System;

namespace Foundation.Messages.Interfaces
{
    public interface IAggregateMessage : IDomainMessage
    {
        Guid AggregateId { get; }
    }
}