using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WWB.AspNetCore;
using WWB.Common.NetCore;
using WWB.Common.Websockets;

namespace WWB.Sample.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]
    public class TestController : BaseApiController
    {
        private readonly ILogger<TestController> _logger;
        private readonly IWebsocketMsgHandler _websocketMsgHandler;

        public TestController(ILogger<TestController> logger, IWebsocketMsgHandler websocketMsgHandler)
        {
            _logger = logger;
            _websocketMsgHandler = websocketMsgHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _websocketMsgHandler.SendMsg("dasf", "");
            return Success();
        }
    }
}