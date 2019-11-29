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
        public UserInfo CurrentUser;
        public ApiBaseController()
        {

        }

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

        protected ApiResult Error(string message = "")
        {
            return new ApiResult(ResultCode.BUSSESS_ERROR, message);
        }

        protected ApiResult Error(ResultCode code, string message = "")
        {
            return new ApiResult(code, message);
        }
    }
}
