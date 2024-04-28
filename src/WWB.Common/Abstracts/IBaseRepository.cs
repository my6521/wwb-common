using SmartSql.DyRepository;

namespace WWB.Common.Abstracts
{
    public interface IBaseRepository<TEntity, K> : IRepository<TEntity, K>, IRepositoryAsync<TEntity, K> where TEntity : class
    {
    }
}