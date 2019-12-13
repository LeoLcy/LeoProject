using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LeoProject.Infrastructure.Controllers.Response;
using LeoProject.LionOA.Api.ViewModel.Role;
using LeoProject.LionOA.Core;
using LeoProject.LionOA.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeoProject.Infrastructure.Extensions;

namespace LeoProject.LionOA.Api.Controllers
{
    public class RoleController : OABaseController
    {
        private readonly ISysRoleService _sysRoleService;
        private readonly ISysRoleModuleService _sysRoleModuleService;
        public RoleController(ISysRoleService sysRoleService,
            ISysRoleModuleService sysRoleModuleService) 
        {
            _sysRoleService = sysRoleService;
            _sysRoleModuleService = sysRoleModuleService;
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
        [HttpGet]
        public async Task<ApiResultPaged<List<SysRole>>> Get(SearchRoleReq req)
        {
            if (req == null)
            {
                return Error<List<SysRole>>("请求参数不能为空");
            }
            Expression<Func<SysRole, bool>> exp = m=>true;
            if (!string.IsNullOrEmpty(req.Name))
            {
                exp = exp.And(m => m.Name.Contains(req.Name));
            }
            var count = _sysRoleService.Count(exp);
            if (count > 0)
            {
                var pagedList = _sysRoleService.GetPagedList(req.PageIndex, req.PageSize, exp);

                var list = await pagedList.ToListAsync();

                return Success(count, list);
            }
            return Success<List<SysRole>>(count,null);
        }
        [HttpGet]
        public async Task<ApiResult> GetOne([FromQuery] int id=0)
        {
            var model = await _sysRoleService.GetByIdAsync(id);
            return Success(model);
        }
        [HttpPost]
        public async Task<ApiResult> Update(RoleReq req)
        {
            if (req == null)
            {
                return Error("请求参数不能为空");
            }
            if (req.Id==0)
            {
                return await Insert(req);
            }

            var model = await _sysRoleService.GetByIdAsync(req.Id);
            model.Name = req.Name;
            model.Remark = req.Remark;
            model.UpdateBy = CurrentUser.UserId;
            model.UpdateTime = DateTime.Now;
            var res = await _sysRoleService.UpdateAsync(model);
            if (res > 0)
            {
                return Success("更新成功");
            }
            return Success("更新失败");
        }
        /// <summary>
        /// 角色分配 模块
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveModules(RoleAddModuleReq req)
        {
            if (req == null)
            {
                return Error("请求参数不能为空");
            }
            if (req.RoleId == 0)
            {
                return Error("请求参数不能为空");
            }
            if (req.Modules == null || req.Modules.Count == 0)
            {
                return Error("请求参数不能为空");
            }
            var role = await _sysRoleService.GetByIdAsync(req.RoleId);
            if (role == null)
            {
                return Error("请求参数出错，查不到当前角色");
            }
            List<SysRoleModule> list = await _sysRoleModuleService.GetQueryable(m => m.RoleId == req.RoleId).ToListAsync();
            //先删除，再增加
            if (list != null && list.Count> 0)
            {
                //更新角色模块权限
                 await _sysRoleModuleService.DeleteRangeAsync(list);
            }
            foreach (var item in req.Modules)
            {
                list.Add(new SysRoleModule
                {
                    RoleId = req.RoleId,
                    ModuleId = item.ModuleId,
                    FuncPermission = item.FuncPermission,
                    CreateBy = CurrentUser.UserId
                });
            }
            var res = await _sysRoleModuleService.InsertRangeAsync(list);
            if (res > 0)
            {
                return Success("更新成功");
            }
            return Success("更新失败");
        }
    }
}