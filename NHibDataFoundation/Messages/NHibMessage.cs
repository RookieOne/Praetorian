using System;

namespace NHibDataFoundation.Messages
{
    public class NHibMessage
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime DateRecieved { get; set; }
        public virtual DateTime DateSent { get; set; }
        public virtual string Message { get; set; }
    }
}