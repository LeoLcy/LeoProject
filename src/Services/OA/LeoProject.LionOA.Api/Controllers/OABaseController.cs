using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
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
        public  IEasyCachingProvider _cache;
        public OABaseController(IEasyCachingProviderFactory factory)
        {
            var  tokenUserInfo = Request.GetTokenUserInfo();
            CurrentUser.UserId = tokenUserInfo.UserId;
            _cache = factory.GetCachingProvider("default");
            var userCache = _cache.Get<UserInfo>($"User${tokenUserInfo.UserId}");
            if (userCache.HasValue)
            {
                CurrentUser = userCache.Value;
            }
            
        }

    }
}