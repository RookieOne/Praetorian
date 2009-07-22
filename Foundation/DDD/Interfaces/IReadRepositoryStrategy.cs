using System;
using System.Linq;
using Foundation.DDD.Interfaces;

namespace Foundation.DDD.Interfaces
{
    public interface IReadRepositoryStrategy : IDisposable
    {
        T GetById<T>(Guid id) where T : IEntity;
        IQueryable<T> GetAll<T>()  where T : IEntity;
    }
}