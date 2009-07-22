using System;

namespace Foundation.Events.Interfaces
{
    public interface IEventHandlerResolver
    {
        object GetEventHandlerFor(Type type);
        object GetEventHandler<T>(T domainEvent);
    }
}