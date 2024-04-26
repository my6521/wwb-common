using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;

namespace WWB.ElasticClient
{
    public static class ElasticClientExtension
    {
        public static string CreateIndex<T>(this ElasticsearchClient client, string prefix) where T : class
        {
            var indexName = $"{prefix}{typeof(T).Name}";
            var existsResponse = client.Indices.ExistsAsync(indexName).GetAwaiter().GetResult();
            if (!existsResponse.Exists)
            {
                var createRequest = new CreateIndexRequest(indexName);

                var response = client.Indices.CreateAsync(createRequest).GetAwaiter().GetResult();
                if (!response.IsValidResponse)
                {
                    throw new Exception("create index error");
                }
            }

            return indexName;
        }
    }
}