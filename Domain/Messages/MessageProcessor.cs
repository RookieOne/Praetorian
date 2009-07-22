using Microsoft.Practices.Unity;

namespace Domain.Messages
{
    public class MessageProcessor
    {
        private static MessageProcessor _messageProcessor;

        public static MessageProcessor Instance { get { return _messageProcessor; } }

        [Dependency]
        public DomainRepository Repository { get; set; }

        [Dependency]
        public IMessageLogger Logger { get; set; }                

        public MessageProcessor()
        {
            Repository = new DomainRepository();
        }

        public static void Setup(IUnityContainer container)
        {
            _messageProcessor = container.Resolve<MessageProcessor>();
        }

        public static void Publish<T>(T message)
        {
            _messageProcessor.HandlePublish(message);
        }

        public void HandlePublish<T>(T message)
        {
            Logger.Log(message as IDomainMessage);

            var consumer = Repository.GetObject(message);
            consumer.Consume(message);
        }
    }
}