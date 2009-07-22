namespace Foundation.Messages.Interfaces
{
    public interface IMessageConsumerRepository
    {
        IConsume<T> GetConsumer<T>(T message);
    }
}