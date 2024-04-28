namespace WWB.Common.Websockets
{
    /// <summary>
    /// Websocket消息处理接口
    /// </summary>
    public interface IWebsocketMsgHandler
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="fromClientId">客户端标识</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        Task ReceiveMsg(string fromClientId, string msg);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="toClientId">客户端标识</param>
        /// <param name="msg">发送消息</param>
        /// <returns></returns>
        Task SendMsg(string toClientId, string msg);
    }
}