using WWB.Common.DI;

namespace WWB.Common.Websockets
{
    /// <summary>
    /// WebSocket消息处理
    /// </summary>
    public class DefaultWebsocketMsgHandler : IWebsocketMsgHandler, IScopedDependency
    {
        /// <summary>
        /// 收到消息处理
        /// </summary>
        /// <param name="fromClientId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task ReceiveMsg(string fromClientId, string msg)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="toClientId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task SendMsg(string toClientId, string msg)
        {
            return Task.CompletedTask;
        }
    }
}