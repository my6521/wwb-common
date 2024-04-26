using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text;
using WWB.Common.Websockets;

namespace WWB.AspNetCore.Middleware
{
    public class WebSocketHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<WebSocketHandlerMiddleware> _logger;

        public WebSocketHandlerMiddleware(RequestDelegate next, ILogger<WebSocketHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    try
                    {
                        var clientId = context.Request.Query["clientid"];
                        var websocket = await context.WebSockets.AcceptWebSocketAsync();
                        if (websocket != null && !WebSocketClientCollection.IsExists(clientId))
                        {
                            var wsclient = new WebSocketClient
                            {
                                ClientId = clientId,
                                WebSocket = websocket
                            };
                            WebSocketClientCollection.AddClient(wsclient);
                            await Handle(context, wsclient);
                        }
                    }
                    catch (Exception ex)
                    {
                        await context.Response.WriteAsync(ex.Message);
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task Handle(HttpContext context, WebSocketClient wsclient)
        {
            WebSocketReceiveResult clientData = null;
            do
            {
                var buffer = new byte[1024 * 10];
                clientData = await wsclient.WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), CancellationToken.None);
                if (clientData.MessageType == WebSocketMessageType.Text)
                {
                    string msgStr = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    await HandlessMessageAsync(context, wsclient.ClientId, msgStr);
                }
            } while (!clientData.CloseStatus.HasValue);
            WebSocketClientCollection.RemoveClient(wsclient.ClientId);
        }

        private async Task HandlessMessageAsync(HttpContext context, string clientId, string useMsg)
        {
            var handler = context.RequestServices.GetService<IWebsocketMsgHandler>();

            await handler.ReceiveMsg(clientId, useMsg);
        }
    }
}