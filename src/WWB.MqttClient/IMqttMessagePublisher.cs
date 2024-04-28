using MQTTnet.Client;
using MQTTnet.Protocol;

namespace WWB.MqttClient
{
    /// <summary>
    /// 发布消息接口
    /// </summary>
    public interface IMqttMessagePublisher
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="qualityOfServiceLevel"></param>
        /// <returns></returns>
        Task<MqttClientPublishResult> Publish(string topic, object data, MqttQualityOfServiceLevel qualityOfServiceLevel);
    }
}