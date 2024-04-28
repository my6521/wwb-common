using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;
using WWB.MqttClient.Internal;

namespace WWB.MqttClient
{
    public static class MqttClientExtensions
    {
        public static IServiceCollection AddMqttClient(this IServiceCollection services, Action<MqttClientOptions> setup)
        {
            var mqttOptions = new MqttClientOptions();
            setup?.Invoke(mqttOptions);
            services.AddSingleton(mqttOptions);

            var clientId = mqttOptions.ClientId;
            var password = mqttOptions.ClientId.Substring(mqttOptions.ClientId.Length - 6);
            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(clientId)
                    .WithCredentials(clientId, password)
                    .WithTcpServer(mqttOptions.Host, mqttOptions.Port)
                    .Build())
                .Build();

            services.AddSingleton(options);
            var mqttClient = new MqttFactory().CreateManagedMqttClient();
            services.AddSingleton<IManagedMqttClient>(mqttClient);
            services.AddSingleton<IMqttMessagePublisher, DefaultMqttMessagePublisher>();
            services.AddSingleton<IMqttMessageReceiver, DefaultMqttMessageReceiver>();

            return services;
        }

        public static IApplicationBuilder UseMqttClient(this IApplicationBuilder app)
        {
            var managedMqttClient = app.ApplicationServices.GetService<IManagedMqttClient>();
            var managedMqttClientOptions = app.ApplicationServices.GetService<ManagedMqttClientOptions>();
            var messageReceiver = app.ApplicationServices.GetService<IMqttMessageReceiver>();
            var options = app.ApplicationServices.GetService<MqttClientOptions>();

            managedMqttClient.ConnectedAsync += e =>
            {
                Console.WriteLine($"client connected!");

                return Task.CompletedTask;
            };

            managedMqttClient.DisconnectedAsync += e =>
            {
                Console.WriteLine($"client disconnected!");

                return Task.CompletedTask;
            };

            managedMqttClient.ApplicationMessageReceivedAsync += (e) =>
            {
                return messageReceiver?.Received(e);
            };

            Console.WriteLine($"MQTT Subscribe topic={options.SubscribeTopics}");

            if (!string.IsNullOrWhiteSpace(options.SubscribeTopics))
            {
                var topics = options.SubscribeTopics
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => new MqttTopicFilter { Topic = x })
                    .ToList();

                managedMqttClient.SubscribeAsync(topics).GetAwaiter().GetResult();
            }

            managedMqttClient.StartAsync(managedMqttClientOptions).GetAwaiter().GetResult();

            return app;
        }
    }
}