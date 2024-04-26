using Microsoft.AspNetCore.Mvc;
using WWB.Common.Dtos;

namespace WWB.Common.NetCore
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult Success<T>(T obj, string msg = "ok")
        {
            var response = new ApiResult<T>
            {
                Data = obj,
                Code = 0,
                Message = msg
            };

            return Ok(response);
        }

        protected IActionResult Success(string msg = "ok")
        {
            var response = new ApiResult
            {
                Code = 0,
                Message = msg
            };

            return Ok(response);
        }

        protected IActionResult Error(string msg = "error")
        {
            var response = new ApiResult
            {
                Code = -100,
                Message = msg
            };

            return Ok(response);
        }
    }
}