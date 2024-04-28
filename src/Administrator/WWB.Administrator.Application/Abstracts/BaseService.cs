using SmartSql;
using WWB.Administrator.Domain.Repository;

namespace WWB.Administrator.Application.Abstracts
{
    public abstract class BaseServiceInt32<TEntity> : BaseService<TEntity, int> where TEntity : class
    {
        protected BaseServiceInt32(IBaseRepository<TEntity, int> repository) : base(repository)
        { }
    }

    public abstract class BaseServiceInt64<TEntity> : BaseService<TEntity, long> where TEntity : class
    {
        protected BaseServiceInt64(IBaseRepository<TEntity, long> repository) : base(repository)
        { }
    }

    public abstract class BaseService<TEntity, TPrimary> where TEntity : class
    {
        public IBaseRepository<TEntity, TPrimary> DbRepository { get; }
        public ISqlMapper SqlMapper { get; }

        protected BaseService(IBaseRepository<TEntity, TPrimary> dbRepositoryy)
        {
            DbRepository = dbRepositoryy;
            SqlMapper = dbRepositoryy.SqlMapper;
        }

        #region Sync

        public int Insert(TEntity entity)
        {
            return DbRepository.Insert(entity);
        }

        public int Update(TEntity entity)
        {
            return DbRepository.Update(entity);
        }

        public int DyUpdate(object dyObj)
        {
            return DbRepository.DyUpdate(dyObj);
        }

        public int Delete(object reqParams)
        {
            return DbRepository.Delete(reqParams);
        }

        public int DeleteById(TPrimary id)
        {
            return DbRepository.DeleteById(id);
        }

        public TEntity GetEntity(object reqParams)
        {
            return DbRepository.GetEntity(reqParams);
        }

        public TEntity GetById(TPrimary id)
        {
            return DbRepository.GetById(id);
        }

        public int GetRecord(object reqParams)
        {
            return DbRepository.GetRecord(reqParams);
        }

        public IList<TEntity> QueryByPage(object reqParams)
        {
            return DbRepository.QueryByPage(reqParams);
        }

        public IList<TEntity> Query(object reqParams)
        {
            return DbRepository.Query(reqParams);
        }

        public bool IsExist(object reqParams)
        {
            return DbRepository.IsExist(reqParams);
        }

        #endregion Sync

        #region Async

        public async Task<int> InsertAsync(TEntity entity)
        {
            return await DbRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await DbRepository.UpdateAsync(entity);
        }

        public async Task<int> DyUpdateAsync(object dyObj)
        {
            return await DbRepository.DyUpdateAsync(dyObj);
        }

        public async Task<int> DeleteAsync(object reqParams)
        {
            return await DbRepository.DeleteAsync(reqParams);
        }

        public async Task<int> DeleteByIdAsync(TPrimary id)
        {
            return await DbRepository.DeleteByIdAsync(id);
        }

        public async Task<TEntity> GetEntityAsync(object reqParams)
        {
            return await DbRepository.GetEntityAsync(reqParams);
        }

        public async Task<TEntity> GetByIdAsync(TPrimary id)
        {
            return await DbRepository.GetByIdAsync(id);
        }

        public async Task<int> GetRecordAsync(object reqParams)
        {
            return await DbRepository.GetRecordAsync(reqParams);
        }

        public async Task<IList<TEntity>> QueryByPageAsync(object reqParams)
        {
            return await DbRepository.QueryByPageAsync(reqParams);
        }

        public async Task<IList<TEntity>> QueryAsync(object reqParams)
        {
            return await DbRepository.QueryAsync(reqParams);
        }

        public async Task<bool> IsExistAsync(object reqParams)
        {
            return await DbRepository.IsExistAsync(reqParams);
        }

        #endregion Async
    }
}