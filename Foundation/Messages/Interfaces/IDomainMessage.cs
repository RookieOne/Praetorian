using System;

namespace Foundation.Messages.Interfaces
{
    public interface IDomainMessage
    {
        Guid Id { get; set; }
        DateTime DateRecieved { get; set; }
        DateTime DateSent { get; set; }
    }
}