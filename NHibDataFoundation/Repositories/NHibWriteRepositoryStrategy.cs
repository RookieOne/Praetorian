using System;
using Foundation.DDD.Interfaces;
using NHibDataFoundation.Configs;

namespace NHibDataFoundation.Repositories
{
    public class NHibWriteRepositoryStrategy : NHibReadRepositoryStrategy,
                                               IWriteRepositoryStrategy
    {
        public NHibWriteRepositoryStrategy(INHibConfig config)
            : base(config)
        {
        }

        public void Save<T>(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Insert<T>(T entity)
        {
            _session.Save(entity);
        }

        public void Delete<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _transaction.Commit();
        }
    }
}