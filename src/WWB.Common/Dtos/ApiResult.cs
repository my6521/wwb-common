namespace WWB.Common.Dtos
{
    public class ApiResult<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ApiResult : ApiResult<object>
    {
        public bool IsSuccess
        {
            get
            {
                return Code == 0;
            }
        }
    }

    public class ErrorApiResult : ApiResult
    {
        public ErrorApiResult()
        {
            Code = 0;
        }

        public ErrorApiResult(int code, string msg) : this()
        {
            Message = msg;
            code = code;
        }
    }
}