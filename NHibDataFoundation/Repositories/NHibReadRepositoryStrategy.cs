using System;
using System.Linq;
using Foundation.DDD.Interfaces;
using NHibDataFoundation.Configs;
using NHibernate;
using NHibernate.Linq;

namespace NHibDataFoundation.Repositories
{
    public class NHibReadRepositoryStrategy : IReadRepositoryStrategy
    {
        protected readonly ISession _session;
        protected readonly ITransaction _transaction;

        public NHibReadRepositoryStrategy(INHibConfig config)
        {
            _session = config.GetSession();

            _transaction = _session.BeginTransaction();
        }

        public T GetById<T>(Guid id) where T : IEntity
        {
            IQueryable<T> entities = from entity in _session.Linq<T>()
                                     where entity.Id == id
                                     select entity;
            return entities.FirstOrDefault();
        }

        public IQueryable<T> GetAll<T>() where T : IEntity
        {
            return from entity in _session.Linq<T>()
                   select entity;
        }

        public void Dispose()
        {
            _session.Dispose();
            _transaction.Dispose();
        }
    }
}