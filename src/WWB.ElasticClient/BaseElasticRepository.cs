using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace WWB.ElasticClient
{
    public abstract class BaseElasticRepository<TEntity> : IElasticRepository<TEntity> where TEntity : BaseElasticEntity
    {
        protected readonly ElasticsearchClient Client;
        protected readonly ElasticOptions Options;
        protected readonly string IndexName;

        public BaseElasticRepository(ElasticsearchClient client, ElasticOptions options)
        {
            Client = client;
            Options = options;
            IndexName = Client.CreateIndex<TEntity>(options.Prefix);
        }

        public async Task<GetResponse<TEntity>> GetAsync(string id)
        {
            var result = await Client.GetAsync<TEntity>(id, action => action.Index(IndexName));

            return result;
        }

        public async Task<IndexResponse> IndexAsync(TEntity document)
        {
            var result = await Client.IndexAsync<TEntity>(document, IndexName, document.Id);

            return result;
        }

        public async Task<UpdateResponse<TEntity>> UpdateAsync(TEntity document)
        {
            var result = await Client.UpdateAsync<TEntity, TEntity>(IndexName, document.Id, u => u.Doc(document));

            return result;
        }

        public async Task<DeleteResponse> DeleteAsync(Id id)
        {
            return await Client.DeleteAsync(IndexName, id);
        }

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