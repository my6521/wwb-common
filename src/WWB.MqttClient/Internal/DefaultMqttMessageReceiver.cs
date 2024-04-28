using MQTTnet.Client;

namespace WWB.MqttClient.Internal
{
    public class DefaultMqttMessageReceiver : IMqttMessageReceiver
    {
        public Task Received(MqttApplicationMessageReceivedEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}