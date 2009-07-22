namespace Foundation.Messages.Interfaces
{
    public interface IMessageLog
    {
        IDomainMessage Log(IDomainMessage message);
    }
}