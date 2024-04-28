using System.Net.WebSockets;

namespace WWB.Common.Websockets
{
    /// <summary>
    /// websocket客户端类
    /// </summary>
    public class WebSocketClient
    {
        /// <summary>
        /// 客户端标识
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// WebSocket
        /// </summary>
        public WebSocket WebSocket { get; set; }
    }
}