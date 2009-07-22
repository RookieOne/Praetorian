using Microsoft.Practices.Unity;

namespace Domain.Events
{
    public class EventProcessor
    {
        private static EventProcessor _eventProcessor;

        public static EventProcessor Instance { get { return _eventProcessor; } }

        [Dependency]
        public IEventRepository EventRepository { get; set; }
        
        public static void Setup(IUnityContainer container)
        {
            _eventProcessor = container.Resolve<EventProcessor>();
        }

        public static void Publish(IDomainEvent domainEvent)
        {
            _eventProcessor.HandleEvent(domainEvent);
        }

        public void HandleEvent(IDomainEvent domainEvent)
        {
            EventRepository.Record(domainEvent);
        }
    }
}