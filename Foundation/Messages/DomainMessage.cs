using System;
using Foundation.Messages.Interfaces;

namespace Foundation.Messages
{
    public abstract class DomainMessage : IDomainMessage
    {
        public DateTime DateRecieved { get; set; }
        public DateTime DateSent { get; set; }
        public Guid Id { get; set; }
    }
}