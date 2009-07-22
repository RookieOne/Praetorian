namespace Foundation.Events
{
    public interface IHandle<T>
    {
        void Handle(T domainEvent);
    }
}