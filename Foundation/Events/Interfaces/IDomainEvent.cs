using System;

namespace Foundation.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; set; }
        DateTime EventDate { get; }
    }
}