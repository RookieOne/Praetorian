using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation.DDD.Interfaces;
using Foundation.Events;
using Foundation.Events.Interfaces;

namespace Foundation.DDD
{
    public class DomainRepository : IDomainRepository
    {
        private readonly IEventHandlerResolver _eventHandlerResolver;
        private readonly IEventRepository _eventRepository;

        public DomainRepository(IEventRepository eventRepository,
                                IEventHandlerResolver eventHandlerResolver)
        {
            _eventRepository = eventRepository;
            _eventHandlerResolver = eventHandlerResolver;
        }

        public IEntity Get(Guid id)
        {
            IEnumerable<IAggregateEvent> domainEvents = _eventRepository.GetAggregateEvents(id);

            object aggregate = null;

            foreach (var domainEvent in domainEvents)
            {
                if (aggregate == null)
                    aggregate = _eventHandlerResolver.GetEventHandler(domainEvent);

                Handle(aggregate, domainEvent);
            }

            return aggregate as IEntity;
        }

        private void Handle<T>(object aggregate, T domainEvent)
        {
            if (aggregate == null)
                return;

            IEnumerable<MethodInfo> methods = aggregate.GetType().GetMethods().Where(m => m.Name == "Handle");

            foreach (var method in methods)
            {
                Type eventType = domainEvent.GetType();
                bool isMethod = method.GetParameters().Any(p => p.ParameterType == eventType);

                if (isMethod)
                {
                    method.Invoke(aggregate, new object[] {domainEvent});
                }
            }
        }
    }
}