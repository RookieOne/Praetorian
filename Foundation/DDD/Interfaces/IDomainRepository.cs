using System;

namespace Foundation.DDD.Interfaces
{
    public interface IDomainRepository
    {
        IEntity Get(Guid id);
    }
}