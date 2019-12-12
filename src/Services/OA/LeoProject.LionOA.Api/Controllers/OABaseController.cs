using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeoProject.Infrastructure.Controllers;
using LeoProject.Infrastructure.Filters;
using LeoProject.LionOA.Api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LeoProject.LionOA.Api.Controllers
{
    [Auth]
    public class OABaseController : ApiBaseController
    {
        public UserInfo CurrentUser;
        public OABaseController()
        {
            var  tokenUserInfo = Request.GetTokenUserInfo();
            if (tokenUserInfo != null)
            {
                CurrentUser.UserId = tokenUserInfo.UserId;

                var userCache = RedisHelper.Get<UserInfo>($"User${tokenUserInfo.UserId}");
                if (userCache != null)
                {
                    CurrentUser = userCache;
                }
            }
        }

    }
}