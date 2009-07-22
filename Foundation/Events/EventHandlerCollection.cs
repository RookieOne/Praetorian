using System;
using System.Collections.Generic;

namespace Foundation.Events
{
    public class EventHandlerCollection
    {
        public EventHandlerCollection()
        {
            _eventHandlers = new Dictionary<Type, List<object>>();
            _subscribers = new List<ISubscribe>();
        }

        private readonly Dictionary<Type, List<object>> _eventHandlers;
        private List<ISubscribe> _subscribers;

        public void Add<T>(IHandle<T> handler)
        {
            Type eventType = typeof (T);

            if (!_eventHandlers.ContainsKey(eventType))
                _eventHandlers.Add(eventType, new List<object>());

            _eventHandlers[eventType].Add(handler);
        }

        public void Add(ISubscribe subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Clear()
        {
            _eventHandlers.Clear();
        }

        public void Handle<T>(T domainEvent)
        {
            Type eventType = typeof (T);

            if (_eventHandlers.ContainsKey(eventType))
            {
                List<object> handlers = _eventHandlers[eventType];

                foreach (object handler in handlers)
                {
                    ((IHandle<T>) handler).Handle(domainEvent);
                }
            }

            foreach (var subscriber in _subscribers)
            {
                subscriber.Handle(domainEvent);
            }
        }
    }
}