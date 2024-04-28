using MQTTnet.Client;

namespace WWB.MqttClient
{
    /// <summary>
    /// 接收消息接口
    /// </summary>
    public interface IMqttMessageReceiver
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task Received(MqttApplicationMessageReceivedEventArgs e);
    }
}