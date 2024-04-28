using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace WWB.ElasticClient
{
    /// <summary>
    /// Elastci仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IElasticRepository<TEntity> where TEntity : BaseElasticEntity
    {
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetResponse<TEntity>> GetAsync(string id);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IndexResponse> IndexAsync(TEntity entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<UpdateResponse<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeleteResponse> DeleteAsync(Id id);

        /// <summary>
        /// 检索实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <param name="query"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        Task<SearchResponse<TEntity>> SearchAsync<TEntity>(int from, int size, Query query, List<SortOptions> sorts);
    }
}