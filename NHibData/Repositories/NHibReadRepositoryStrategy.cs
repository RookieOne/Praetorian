using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.DDD;
using NHibData.Configs;
using NHibernate;

namespace NHibData.Repositories
{
    public class NHibReadRepositoryStrategy : IReadRepositoryStrategy
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public NHibReadRepositoryStrategy(INHibernateConfig config)
        {
            //@"LUBU\SQLExpress2"
            //"GreekFireExpenseDB"
            MsSqlConfiguration sqlConfig = MsSqlConfiguration.MsSql2005
                .ConnectionString(c => c.Server(config.Server)
                                           .Database(config.DatabaseName)
                                           .TrustedConnection());

            ISessionFactory factory = Fluently.Configure()
                .Database(sqlConfig)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateRepositoryStrategy>())
                .BuildSessionFactory();

            _session = factory.OpenSession();

            _transaction = _session.BeginTransaction();
        }

        public IQueryable<T> GetAll<T>()
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
