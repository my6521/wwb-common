using WWB.Common.Websockets;

namespace WWB.Sample.API.Handler
{
    public class WsMsgHandler : IWebsocketMsgHandler
    {
        public Task ReceiveMsg(string fromClientId, string msg)
        {
            throw new NotImplementedException();
        }

        public Task SendMsg(string toClientId, string msg)
        {
            throw new NotImplementedException();
        }
    }
}