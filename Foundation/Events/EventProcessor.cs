using Microsoft.Practices.Unity;

namespace Foundation.Events
{
    public class EventProcessor
    {
        private static readonly EventHandlerCollection _eventHandlers;
        private static EventProcessor _eventProcessor;

        [Dependency]
        public IEventRepository EventRepository { get; set; }

        static EventProcessor()
        {
            _eventHandlers = new EventHandlerCollection();
        }

        public static void Setup(IUnityContainer container)
        {
            _eventProcessor = container.Resolve<EventProcessor>();
        }

        public static void Publish<T>(T domainEvent)
        {
            _eventProcessor.Handle(domainEvent);
        }

        public static void Subscribe<T>(IHandle<T> handler)
        {
            _eventHandlers.Add(handler);
        }

        public static void SubscribeToAll(ISubscribe subscriber)
        {
            _eventHandlers.Add(subscriber);
        }

        public static void Clear()
        {
            _eventHandlers.Clear();
        }

        public void Handle<T>(T domainEvent)
        {
            EventRepository.Record(domainEvent as IDomainEvent);
            _eventHandlers.Handle(domainEvent);
        }
    }
}