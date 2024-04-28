namespace WWB.MqttClient
{
    /// <summary>
    /// MQTT客户端配置
    /// </summary>
    public class MqttClientOptions
    {
        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 订阅主题，多个用“,”号分割
        /// </summary>
        public string SubscribeTopics { get; set; }
    }
}