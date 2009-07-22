using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation.Events;
using Foundation.Events.Interfaces;

namespace Foundation.DDD
{
    public class WriteRepositoryEventSubscriber : ISubscribe
    {
        private readonly IEventHandlerResolver _eventHandlerResolver;

        public WriteRepositoryEventSubscriber(IEventHandlerResolver eventHandlerResolver)
        {
            _eventHandlerResolver = eventHandlerResolver;
        }

        public void Handle(object domainEvent)
        {
            object handler = _eventHandlerResolver.GetEventHandlerFor(domainEvent.GetType());

            if (handler == null)
                return;

            IEnumerable<MethodInfo> methods = handler.GetType().GetMethods().Where(m => m.Name == "Handle");

            foreach (var method in methods)
            {
                Type eventType = domainEvent.GetType();
                bool isMethod = method.GetParameters().Any(p => p.ParameterType == eventType);

                if (isMethod)
                {
                    method.Invoke(handler, new[] {domainEvent});
                }
            }
        }
    }
}