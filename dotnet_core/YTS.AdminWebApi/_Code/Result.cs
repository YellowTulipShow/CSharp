namespace YTS.AdminWebApi
{
    public class Result
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// 直接输出错误提示
        /// </summary>
        /// <param name="message">错误内容</param>
        /// <returns>返回一个指定错误内容的默认错误结果对象</returns>
        public static Result Error(string message)
        {
            return new Result()
            {
                ErrorCode = 1,
                Message = message,
            };
        }

        /// <summary>
        /// 直接输出成功提示
        /// </summary>
        /// <param name="message">成功内容</param>
        /// <returns>返回一个指定成功内容的默认成功结果对象</returns>
        public static Result Success(string message)
        {
            return new Result()
            {
                ErrorCode = 0,
                Message = message,
            };
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        /// <summary>
        /// 直接输出错误提示
        /// </summary>
        /// <param name="message">错误内容</param>
        /// <returns>返回一个指定错误内容的默认错误结果对象</returns>
        public new static Result<T> Error(string message)
        {
            return new Result<T>()
            {
                ErrorCode = 1,
                Data = default(T),
                Message = message,
            };
        }

        /// <summary>
        /// 直接输出成功提示
        /// </summary>
        /// <param name="message">成功内容</param>
        /// <returns>返回一个指定成功内容的默认成功结果对象</returns>
        public static Result<T> Success(string message, T data)
        {
            return new Result<T>()
            {
                ErrorCode = 0,
                Data = data,
                Message = message,
            };
        }
    }
}