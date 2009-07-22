using System;

namespace NHibDataFoundation.Events
{
    public class NHibEvent
    {
        public virtual Guid Id { get; set; }
        public virtual string Event { get; set; }
        public virtual DateTime EventDate { get; set; }
    }
}