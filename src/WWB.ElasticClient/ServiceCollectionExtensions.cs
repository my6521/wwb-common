using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.DependencyInjection;

namespace WWB.ElasticClient
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services, ElasticOptions options)
        {
            services.AddSingleton(options);

            services.AddSingleton<ElasticsearchClient>(sp =>
            {
                var settings = new ElasticsearchClientSettings(new Uri(options.Url));
                //settings.CertificateFingerprint("<FINGERPRINT>");
                settings.Authentication(new BasicAuthentication(options.UserName, options.Password));

                return new ElasticsearchClient(settings);
            });

            return services;
        }
    }
}