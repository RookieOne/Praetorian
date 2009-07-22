using System;
using Foundation.DDD;

namespace NHibData.Repositories
{
    public class NHibRepositoryStrategy : IDatabaseRepositoryStrategy
    {
        public void Save<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}