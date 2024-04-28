using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using Newtonsoft.Json;

namespace WWB.MqttClient.Internal
{
    public class DefaultMqttMessagePublisher : IMqttMessagePublisher
    {
        private readonly IManagedMqttClient _managedMqttClient;

        public DefaultMqttMessagePublisher(IManagedMqttClient managedMqttClient)
        {
            _managedMqttClient = managedMqttClient;
        }

        public async Task<MqttClientPublishResult> Publish(string topic, object data, MqttQualityOfServiceLevel qualityOfServiceLevel)
        {
            string payload = JsonConvert.SerializeObject(data);

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(qualityOfServiceLevel)
                .WithRetainFlag(false)
                .Build();

            var result = await _managedMqttClient.InternalClient.PublishAsync(message, CancellationToken.None);

            return result;
        }
    }
}