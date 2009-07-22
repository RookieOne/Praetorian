using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.DDD.Interfaces;

namespace Foundation_Test.Mocks
{
    public class MockWriteRepositoryStrategy : IWriteRepositoryStrategy
    {
        public MockWriteRepositoryStrategy()
        {
            _entities = new Dictionary<Type, List<IEntity>>();
        }

        private readonly Dictionary<Type, List<IEntity>> _entities;

        public void Commit()
        {
        }

        public void Delete<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IQueryable<T> GetAll<T>() where T : IEntity
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(Guid id) where T : IEntity
        {
            if (!_entities.ContainsKey(typeof (T)))
                return default(T);

            return (T) _entities[typeof (T)].FirstOrDefault(e => e.Id == id);
        }

        public void Insert<T>(T entity)
        {
            Save(entity);
        }

        public void Save<T>(T entity)
        {
            if (!_entities.ContainsKey(typeof (T)))
                _entities.Add(typeof (T), new List<IEntity>());

            var entitiesList = _entities[typeof (T)];

            var castedEntity = entity as IEntity;
            var found = entitiesList.FirstOrDefault(e => e.Id == castedEntity.Id);

            if (found == null)
                entitiesList.Add(castedEntity);
        }
    }
}