using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WWB.AspNetCore;
using WWB.Common.Attributes;
using WWB.Common.NetCore;

namespace WWB.Sample.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]
    public class TestController : BaseApiController
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [RedisLock]
        public IActionResult Get()
        {
            return Success();
        }
    }
}