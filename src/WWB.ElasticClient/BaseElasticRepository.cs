using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace WWB.ElasticClient
{
    /// <summary>
    /// Elastic仓库抽象类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseElasticRepository<TEntity> : IElasticRepository<TEntity> where TEntity : BaseElasticEntity
    {
        /// <summary>
        /// ElasticClient客户端
        /// </summary>
        public ElasticsearchClient Client { get; private set; }

        /// <summary>
        /// 配置
        /// </summary>
        public ElasticOptions Options { get; private set; }

        /// <summary>
        /// 索引名
        /// </summary>
        protected string IndexName { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="options"></param>
        public BaseElasticRepository(ElasticsearchClient client, ElasticOptions options)
        {
            Client = client;
            Options = options;
            IndexName = Client.CreateIndex<TEntity>(options.Prefix);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetResponse<TEntity>> GetAsync(string id)
        {
            var result = await Client.GetAsync<TEntity>(id, action => action.Index(IndexName));

            return result;
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IndexResponse> IndexAsync(TEntity entity)
        {
            var result = await Client.IndexAsync<TEntity>(entity, IndexName, entity.Id);

            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<UpdateResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            var result = await Client.UpdateAsync<TEntity, TEntity>(IndexName, entity.Id, u => u.Doc(entity));

            return result;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeleteResponse> DeleteAsync(Id id)
        {
            return await Client.DeleteAsync(IndexName, id);
        }

        /// <summary>
        /// 检索实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <param name="query"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public async Task<SearchResponse<TEntity>> SearchAsync<TEntity>(int from, int size, Query query, List<SortOptions> sorts)
        {
            var searchRequest = new SearchRequest<TEntity>(Indices.Index(IndexName))
            {
                From = from,
                Size = size,
                Query = query,
                Sort = sorts,
            };

            return await Client.SearchAsync<TEntity>(searchRequest);
        }
    }
}