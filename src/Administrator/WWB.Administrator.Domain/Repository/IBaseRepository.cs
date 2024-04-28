using SmartSql.DyRepository;

namespace WWB.Administrator.Domain.Repository
{
    public interface IBaseRepository<TEntity, K> : IRepository<TEntity, K>, IRepositoryAsync<TEntity, K> where TEntity : class
    {
    }
}