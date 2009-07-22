using System;

namespace Domain.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }
    }
}