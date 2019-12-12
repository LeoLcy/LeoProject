using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeoProject.Infrastructure.Controllers.Response;
using LeoProject.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LeoProject.Infrastructure.Filters
{
    public class AuthAttribute :TypeFilterAttribute
    {
        public AuthAttribute() : base(typeof(AuthFilter))
        {
            Order = 0;
        }
    }
    public class AuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                context.HttpContext.Items["AllowAnonymous"] = true;
                return;
            }
            context.HttpContext.Items["AllowAnonymous"] = false;
            var token = context.HttpContext.Request.Headers["token"].ToString();
            if (string.IsNullOrEmpty(token))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                var res = JsonConvert.SerializeObject(new ApiResult(ResultCode.Forbidden_ERROR, "token不存在"));
                context.HttpContext.Response.WriteAsync(res);
                return;
            }
            var userInfoStr = TokenHelper.GetDecodeTokenString(token);

            TokenUserInfo userInfo = null;
            try
            {
                userInfo = JsonConvert.DeserializeObject<TokenUserInfo>(userInfoStr);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                var res = JsonConvert.SerializeObject(new ApiResult(ResultCode.Forbidden_ERROR, "token无效"));
                context.HttpContext.Response.WriteAsync(res);
                throw ex;
            }
            if (userInfo != null&& userInfo.UserId>0)
            {
                if (DateTimeHelper.UnixTimeStampToDateTime(userInfo.ExpiredTimestamp) <
                      DateTime.Now)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    var res = JsonConvert.SerializeObject(new ApiResult(ResultCode.Forbidden_ERROR, "token已过期"));
                    context.HttpContext.Response.WriteAsync(res);
                    return ;
                }
                context.HttpContext.Items["TokenUserInfo"] = userInfo;
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                var res = JsonConvert.SerializeObject(new ApiResult(ResultCode.Forbidden_ERROR, "token无效"));
                context.HttpContext.Response.WriteAsync(res);
                return;
            }
        }
    }
    /// <summary>
    /// token中保存的用户基本信息
    /// 保存可以查到用户信息的标识
    /// </summary>
    public class TokenUserInfo
    {
        /// <summary>
        /// 用户的Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户的Id
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// OpenId，微信OpenId
        /// </summary>
        public string OpenId { get; set; }

        [JsonProperty("exp")]
        public long ExpiredTimestamp { get; set; }

        [JsonProperty("iat")]
        public long IssueTimestamp { get; set; }
    }
    public static class HttpRequestExt
    {
        /// <summary>
        /// 获取token中的用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static TokenUserInfo GetTokenUserInfo(this HttpRequest request)
        {
            if (request.HttpContext.Items.ContainsKey("TokenUserInfo"))
            {
                return request.HttpContext.Items["TokenUserInfo"] as TokenUserInfo;
            }
            string token = request.Headers["token"];

            var userInfoStr = TokenHelper.GetDecodeTokenString(token);

            TokenUserInfo userInfo = null;
            try
            {
                if (!string.IsNullOrEmpty(userInfoStr))
                {
                    userInfo = JsonConvert.DeserializeObject<TokenUserInfo>(userInfoStr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userInfo;
        }
    }
}
