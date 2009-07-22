namespace Domain.Events
{
    public interface IEventHandler<T>
    {
        void Handle(T domainEvent);
    }
}