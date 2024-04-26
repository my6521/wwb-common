using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace WWB.ElasticClient
{
    public interface IElasticRepository<TEntity> where TEntity : BaseElasticEntity
    {
        Task<GetResponse<TEntity>> GetAsync(string id);

        Task<IndexResponse> IndexAsync(TEntity document);

        Task<UpdateResponse<TEntity>> UpdateAsync(TEntity document);

        Task<DeleteResponse> DeleteAsync(Id id);

        Task<SearchResponse<TEntity>> SearchAsync<TEntity>(int from, int size, Query query, List<SortOptions> sorts);
    }
}