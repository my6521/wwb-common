using WWB.Common.Extensions;

namespace WWB.Common
{
    /// <summary>
    /// 友好异常
    /// </summary>
    public class FriendlyException : Exception
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public FriendlyException() : base()
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">错误描述</param>
        public FriendlyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">错误描述</param>
        /// <param name="innerException">内部异常</param>
        public FriendlyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        /// <param name="message">错误描述</param>
        public FriendlyException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        /// <param name="message">错误描述</param>
        /// <param name="innerException">内部异常</param>
        public FriendlyException(int errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 格式化异常
        /// </summary>
        /// <typeparam name="T">错误枚举</typeparam>
        /// <param name="errorCode">错误编码</param>
        /// <param name="formatTexts">格式化文本</param>
        /// <returns></returns>
        public static FriendlyException Of<T>(T errorCode, params string[] formatTexts) where T : Enum
        {
            var error = errorCode.GetDescription();
            var message = error.Item2;
            if (!string.IsNullOrEmpty(message) && formatTexts.Length > 0)
            {
                message = string.Format(message, formatTexts);
            }

            return new FriendlyException(error.Item1, message);
        }
    }
}