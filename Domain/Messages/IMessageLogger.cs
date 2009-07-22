namespace Domain.Messages
{
    public interface IMessageLogger
    {
        void Log(IDomainMessage message);
    }
}