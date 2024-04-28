namespace WWB.MqttClient
{
    public class MqttOptions
    {
        public string ClientId { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string SubscribeTopics { get; set; }
    }
}