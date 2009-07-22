using System;
using System.Collections.Generic;

namespace Domain.Events
{
    public interface IEventRepository
    {
        void Record(IDomainEvent domainEvent);
        IEnumerable<IDomainEvent> GetEvents(Guid id);
    }
}