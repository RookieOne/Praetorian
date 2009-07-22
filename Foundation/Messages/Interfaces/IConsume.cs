namespace Foundation.Messages.Interfaces
{
    public interface IConsume<T>
    {
        void Consume(T message);
    }
}