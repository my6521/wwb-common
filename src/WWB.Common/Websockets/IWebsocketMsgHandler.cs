namespace WWB.Common.Websockets
{
    public interface IWebsocketMsgHandler
    {
        Task ReceiveMsg(string fromClientId, string msg);

        Task SendMsg(string toClientId, string msg);
    }
}