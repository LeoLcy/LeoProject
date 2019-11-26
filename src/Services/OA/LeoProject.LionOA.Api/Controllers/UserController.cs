using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeoProject.Infrastructure;
using LeoProject.Infrastructure.Controllers;
using LeoProject.LionOA.Api.ViewModel.Request;
using LeoProject.LionOA.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeoProject.LionOA.Api.Controllers
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public class UserController : ApiBaseController
    {
        private ISysUserService _userService;
        public UserController(ISysUserService userService)
        {
            _userService = userService;

        }

        public async Task Login(LoginReq login)
        {
            var password = Md5.Encrypt(login.Password);
            var user = await _userService.FindFirstOrDefaultAsync(m => m.UserName == login.UserName && m.Password == password);
            if (user == null)
            {
                return ;
            }
            return;
        }
    }
}