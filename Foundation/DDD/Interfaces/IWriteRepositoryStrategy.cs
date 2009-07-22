namespace Foundation.DDD.Interfaces
{
    public interface IWriteRepositoryStrategy : IReadRepositoryStrategy
    {
        void Save<T>(T entity);

        void Delete<T>(T entity);

        void Insert<T>(T entity);

        void Commit();
    }
}