using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LeoProject.Infrastructure.Controllers;
using LeoProject.Infrastructure.Controllers.Response;
using LeoProject.Infrastructure.Helpers;
using LeoProject.Infrastructure.Tree;
using LeoProject.LionOA.Api.ViewModel;
using LeoProject.LionOA.Api.ViewModel.Role;
using LeoProject.LionOA.Core;
using LeoProject.LionOA.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeoProject.Infrastructure.Extensions;

namespace LeoProject.LionOA.Api.Controllers
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public class UserController : OABaseController
    {
        private readonly ISysUserService _userService;
        private readonly ISysUserDeptService _userDeptService;
        private readonly ISysUserRoleService _userRoleService;
        private readonly ISysRoleModuleService _roleModuleService;
        private readonly ISysModuleService _moduleService;
        private readonly ISysDeptRoleService _deptRoleService;
        public UserController(ISysUserService userService,
            ISysUserDeptService userDeptService,
            ISysUserRoleService userRoleService,
            ISysRoleModuleService roleModuleService,
            ISysModuleService moduleService,
            ISysDeptRoleService deptRoleService)
        {
            _userService = userService;
            _userDeptService = userDeptService;
            _userRoleService = userRoleService;
            _roleModuleService = roleModuleService;
            _moduleService = moduleService;
            _deptRoleService = deptRoleService;
        }
        [HttpPost]
        public async Task<ApiResult> Insert(UserInsertReq req)
        {
            if (req == null)
            {
                return Error("请求参数不能为空");
            }

            SysUser role = new SysUser
            {
                UserName = req.UserName,
                Password = Md5Helper.Encrypt(req.Password),
                RealName = req.RealName,
                NickName = req.NickName,
                Avator = req.Avator,
                Mobile = req.Mobile,
                Email = req.Email,
                Remark = req.Remark,
                CreateBy = CurrentUser.UserId
            };
            var res = await _userService.InsertAsync(role);
            if (res > 0)
            {
                return Success("添加成功");
            }
            return Success("添加失败");
        }
        [HttpGet]
        public async Task<ApiResultPaged<List<SysUser>>> Get(SearchUserReq req)
        {
            if (req == null)
            {
                return Error<List<SysUser>>("请求参数不能为空");
            }
            Expression<Func<SysUser, bool>> exp = m => true;
            if (!string.IsNullOrEmpty(req.UserName))
            {
                exp = exp.And(m => m.UserName.Contains(req.UserName));
            }
            if (!string.IsNullOrEmpty(req.RealName))
            {
                exp = exp.And(m => m.RealName.Contains(req.RealName));
            }
            if (!string.IsNullOrEmpty(req.NickName))
            {
                exp = exp.And(m => m.NickName.Contains(req.NickName));
            }
            if (!string.IsNullOrEmpty(req.Mobile))
            {
                exp = exp.And(m => m.Mobile.Contains(req.Mobile));
            }
            if (!string.IsNullOrEmpty(req.Email))
            {
                exp = exp.And(m => m.Email.Contains(req.Email));
            }
            var count = _userService.Count(exp);
            if (count > 0)
            {
                var pagedList = _userService.GetPagedList(req.PageIndex, req.PageSize, exp);

                var list = await pagedList.ToListAsync();

                return Success(count, list);
            }
            return Success<List<SysUser>>(count, null);
        }
        [HttpGet]
        public async Task<ApiResult> GetOne([FromQuery] int id = 0)
        {
            var model = await _userService.GetByIdAsync(id);
            return Success(model);
        }
        [HttpPost]
        public async Task<ApiResult> Update(UserUpdateReq req)
        {
            if (req == null)
            {
                return Error("请求参数不能为空");
            }
            if (req.Id == 0)
            {
                return Error("请求参数Id不能为空");
            }

            var model = await _userService.GetByIdAsync(req.Id);
            model.NickName = req.NickName;
            model.Avator = req.Avator;
            model.Mobile = req.Mobile;
            model.Email = req.Email;
            model.Remark = req.Remark;
            model.UpdateBy = CurrentUser.UserId;
            model.UpdateTime = DateTime.Now;
            var res = await _userService.UpdateAsync(model);
            if (res > 0)
            {
                return Success("更新成功");
            }
            return Success("更新失败");
        }


    }
}