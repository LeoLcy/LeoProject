using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using LeoProject.Infrastructure.Controllers;
using LeoProject.Infrastructure.Controllers.Response;
using LeoProject.LionOA.Api.ViewModel.Request.Role;
using LeoProject.LionOA.Core;
using LeoProject.LionOA.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeoProject.LionOA.Api.Controllers
{
    public class RoleController : OABaseController
    {
        private readonly ISysRoleService _sysRoleService;
        public RoleController(ISysRoleService sysRoleService,
            IEasyCachingProviderFactory factory) : base(factory)
        {
            _sysRoleService = sysRoleService;
        }
        [HttpPost]
        public async Task<ApiResult> Insert(RoleReq req)
        {
            if (req == null)
            {
                return Error("请求参数不能为空");
            }
            if (string.IsNullOrEmpty(req.Name))
            {
                return Error("名字不能为空");
            }

            SysRole role = new SysRole
            {
                Name = req.Name,
                Remark = req.Remark,
                CreateBy = CurrentUser.UserId
            };
            var res = await _sysRoleService.InsertAsync(role);
            if (res > 0)
            {
                return Success("添加成功");
            }
            return Success("添加失败");
        }
    }
}