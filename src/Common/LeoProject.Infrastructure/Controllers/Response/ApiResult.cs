using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LeoProject.Infrastructure.Controllers.Response
{
    public class ApiResult : ApiResult<object>
    {
        public ApiResult(ResultCode code, string message) : base(code,message)
        {

        }
        public ApiResult(ResultCode code, string message,object data) : base(code, message,data)
        {

        }
    }
    public class ApiResult<T>
    {
        public ApiResult(ResultCode code,string message)
        {
            Code = code;
            Message = message;
        }
        public ApiResult(ResultCode code, string message,T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
        public ResultCode Code { get; set; }
        /// <summary>
        /// 返回值信息
        /// </summary>
        public string Message { get; set; }
        public T Data { get; set; }

    }

    public class ApiResultPaged<T>:ApiResult<T>
    {
        /// <summary>
        /// 分页总数
        /// </summary>
        public int Total { get; set; }
        public ApiResultPaged(int total,T data):base(ResultCode.SUCCESS, "", data)
        {
            Total = total;
        }
    }
    /// <summary>
    /// 返回值Code
    /// 成功：10000，
    /// 业务提示：在业务中定义，10001开始，附上Message
    /// 出现异常提示：20001开始，附上Message提示
    /// </summary>
    public enum ResultCode:int
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        [Description("服务正在更新中,请稍后再试")]
        UNKNOWN_ERROR = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        SUCCESS = 10000,
        /// <summary>
        /// 业务异常
        /// </summary>
        [Description("业务异常")]
        BUSSESS_ERROR = 10001,
        /// <summary>
        /// 异常报错，异常错误
        /// </summary>
        [Description("异常错误,请稍后再试")]
        Exception_ERROR = 20001,
    }
}
