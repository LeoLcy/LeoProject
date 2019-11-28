using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeoProject.Infrastructure;
using LeoProject.Infrastructure.Controllers;
using LeoProject.Infrastructure.Controllers.Response;
using LeoProject.Infrastructure.Tree;
using LeoProject.LionOA.Api.ViewModel.Request;
using LeoProject.LionOA.Api.ViewModel.Response;
using LeoProject.LionOA.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeoProject.LionOA.Api.Controllers
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public class UserController : ApiBaseController
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

        public async Task<ApiResult> Login(LoginReq login)
        {
            if (string.IsNullOrEmpty(login.UserName))
            {
                return Error("用户名不能为空");
            }
            if (string.IsNullOrEmpty(login.Password))
            {
                return Error("密码不能为空");
            }
            var password = Md5.Encrypt(login.Password);
            var user = await _userService.FindFirstOrDefaultAsync(m => m.UserName == login.UserName && m.Password == password);
            if (user == null)
            {
                return Error("用户名或密码错误");
            }
            LoginUserRes loginUserRes = new LoginUserRes
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Avator = user.Avator,
                Mobile = user.Mobile,
                Email = user.Email,
                Depts = new List<long>(),
                Roles = new List<long>(),
                GrantedModule = new GrantedModuleTree(),
                ModuleList = new List<SysModuleRes>()
            };
            loginUserRes.Depts = await GetUserDepts(user.Id);
            loginUserRes.Roles = await GetUserRoles(user.Id, loginUserRes.Depts);
            loginUserRes.ModuleList = await GetUserModules(loginUserRes.Roles);
            var treeNode = new GrantedModuleTree();
            loginUserRes.GrantedModule = TreeHelper.ListToTree(treeNode, loginUserRes.ModuleList);
            return Success(loginUserRes);
        }
        private async Task<List<long>> GetUserDepts(long userId)
        {
            var depts = await _userDeptService.GetQueryable(m => m.UserId == userId).Select(m => m.DeptId).ToListAsync();

            return depts;
        }
        private async Task<List<long>> GetUserRoles(long userId, List<long> depts)
        {
            var temp = _userRoleService.GetQueryable(m => m.UserId == userId).Select(m => m.RoleId)
                .Union(_deptRoleService.GetQueryable(m => depts.Contains(m.DeptId)).Select(m => m.RoleId));
            var roles = await temp.ToListAsync();
            return roles;
        }
        private async Task<List<SysModuleRes>> GetUserModules(List<long> roles)
        {
            var temp = from relation in _roleModuleService.GetQueryable(m => roles.Contains(m.RoleId)).Select(m => new { m.ModuleId,m.FuncPermission })
                       join module in _moduleService.GetQueryable() on relation.ModuleId equals module.Id
                       select new SysModuleRes {
                           Id = module.Id,
                           Name = module.Name,
                           ParentId = module.ParentId,
                           ParentName = module.ParentName,
                           TreePath = module.ParentName,
                           Url = module.Url,
                           Component = module.Component,
                           FuncPermission = relation.FuncPermission,
                           Icon = module.Icon,
                           IsLeaf = module.IsLeaf,
                           Sort = module.Sort,
                           IsAutoExpand = module.IsAutoExpand,
                           Remark = module.Remark
                       };

            var modules = await temp.ToListAsync();
            return modules;
        }
        //private SysModuleRes TranslateToRes(SysModule)
        //{
        //    return null;
        //}

    }
}