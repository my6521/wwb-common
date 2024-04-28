namespace WWB.Common.Abstracts
{
    public interface IBaseServiceInt32<TEntity> : IBaseRepository<TEntity, int> where TEntity : class
    {
    }

    public interface IBaseServiceInt64<TEntity> : IBaseRepository<TEntity, long> where TEntity : class
    {
    }

    public interface IBaseService<TEntity, TPrimary> : IBaseRepository<TEntity, TPrimary> where TEntity : class
    {
        IBaseRepository<TEntity, TPrimary> DbRepository { get; }
    }
}