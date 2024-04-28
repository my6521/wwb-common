namespace WWB.Common.Websockets
{
    /// <summary>
    /// WebSocket客户端管理
    /// </summary>
    public class WebSocketClientCollection
    {
        private static List<WebSocketClient> _client = new List<WebSocketClient>();

        public static void AddClient(WebSocketClient client)
        {
            var clientNow = _client.Where(c => c.ClientId == client.ClientId).FirstOrDefault();
            if (clientNow != null)
                _client.Remove(client);
            _client.Add(client);
        }

        public static void RemoveClient(string clientId)
        {
            var clientNow = _client.Where(c => c.ClientId == clientId).FirstOrDefault();
            if (clientNow != null)
                _client.Remove(clientNow);
        }

        public static WebSocketClient GetClient(string clientId)
        {
            return _client.FirstOrDefault(c => c.ClientId == clientId);
        }

        public static List<WebSocketClient> GetAllClient()
        {
            return _client;
        }

        public static bool IsExists(string clientId)
        {
            return _client.FirstOrDefault(c => c.ClientId == clientId) != null;
        }
    }
}