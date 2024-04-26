using WWB.Common.DI;

namespace WWB.Common.Websockets
{
    public class DefaultWebsocketMsgHandler : IWebsocketMsgHandler, IScopedDependency
    {
        public Task ReceiveMsg(string fromClientId, string msg)
        {
            return Task.CompletedTask;
        }

        public Task SendMsg(string toClientId, string msg)
        {
            return Task.CompletedTask;
        }
    }
}