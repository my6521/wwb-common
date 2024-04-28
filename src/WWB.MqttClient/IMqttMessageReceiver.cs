using MQTTnet.Client;

namespace WWB.MqttClient
{
    public interface IMqttMessageReceiver
    {
        Task Received(MqttApplicationMessageReceivedEventArgs e);
    }
}