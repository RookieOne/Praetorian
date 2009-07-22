using System;

namespace Foundation.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            Id = Guid.NewGuid();
        }

        public virtual DateTime EventDate { get; set; }
        public virtual Guid Id { get; set; }
    }
}