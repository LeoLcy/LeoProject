using LeoProject.Infrastructure.Controllers.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.Infrastructure.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiBaseController:ControllerBase
    {
        public ApiBaseController()
        {

        }
        #region 成功
        protected ApiResult Success(string message = "")
        {
            return new ApiResult(ResultCode.SUCCESS, message);
        }
        protected ApiResult Success<T>(T data)
        {
            return new ApiResult(ResultCode.SUCCESS, "", data);
        }
        protected ApiResult Success<T>(T data, string message = "")
        {
            return new ApiResult(ResultCode.SUCCESS, message,data) ;
        }
        #endregion
        #region 分页数据返回
        protected ApiResultPaged<T> Success<T>(int total,T data)
        {
            return new ApiResultPaged<T>(total,data);
        }
        protected ApiResultPaged<T> Success<T>(int total, T data, string msg = "")
        {
            return new ApiResultPaged<T>(total, data,msg);
        }
        #endregion

        protected ApiResult Error(string message = "")
        {
            return new ApiResult(ResultCode.BUSSESS_ERROR, message);
        }

        protected ApiResult Error(ResultCode code, string message = "")
        {
            return new ApiResult(code, message);
        }
        protected ApiResultPaged<T> Error<T>(string message = "")
        {
            return new ApiResultPaged<T>(ResultCode.BUSSESS_ERROR, message);
        }
        protected ApiResultPaged<T> Error<T>(ResultCode code, string message = "")
        {
            return new ApiResultPaged<T>(code, message);
        }
    }
}
