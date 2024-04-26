using Elastic.Clients.Elasticsearch;

namespace WWB.ElasticClient
{
    public interface IElasticRepository
    {
        /// <summary>
        /// 添加索引文档
        /// </summary>
        /// <typeparam name="TDocument">文档对象泛型</typeparam>
        /// <param name="document">文档对象</param>
        /// <param name="index">索引名称</param>
        /// <returns></returns>
        Task<IndexResponse> IndexAsync<TDocument>(TDocument document, IndexName index, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 获取文档
        /// </summary>
        /// <typeparam name="TDocument">文档对象</typeparam>
        /// <param name="id">文档id</param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetResponse<TDocument>> GetAsync<TDocument>(Id id, Action<GetRequestDescriptor<TDocument>> action, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 搜索文档
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SearchResponse<TDocument>> SearchAsync<TDocument>(Action<SearchRequestDescriptor<TDocument>> action, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 更新索引
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TPartialDocument"></typeparam>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="index"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeleteResponse> DeleteAsync<TDocument>(IndexName index, Id id, CancellationToken cancellationToken = default(CancellationToken));
    }
}