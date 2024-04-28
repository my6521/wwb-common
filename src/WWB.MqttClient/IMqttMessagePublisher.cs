using MQTTnet.Client;
using MQTTnet.Protocol;

namespace WWB.MqttClient
{
    public interface IMqttMessagePublisher
    {
        Task<MqttClientPublishResult> Publish(string topic, object data, MqttQualityOfServiceLevel qualityOfServiceLevel);
    }
}