using System.Net.WebSockets;

namespace WWB.Common.Websockets
{
    public class WebSocketClient
    {
        public string ClientId { get; set; }
        public WebSocket WebSocket { get; set; }
    }
}