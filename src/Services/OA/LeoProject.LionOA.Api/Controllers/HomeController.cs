using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using LeoProject.Infrastructure;
using LeoProject.Infrastructure.Controllers;
using LeoProject.Infrastructure.Controllers.Response;
using LeoProject.Infrastructure.Helpers;
using LeoProject.Infrastructure.Tree;
using LeoProject.LionOA.Api.ViewModel;
using LeoProject.LionOA.Api.ViewModel.Request;
using LeoProject.LionOA.Api.ViewModel.Response;
using LeoProject.LionOA.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LeoProject.LionOA.Api.Controllers
{
    public class HomeController : OABaseController
    {
        private readonly ISysUserService _userService;
        private readonly ISysUserDeptService _userDeptService;
        private readonly ISysUserRoleService _userRoleService;
        private readonly ISysRoleModuleService _roleModuleService;
        private readonly ISysModuleService _moduleService;
        private readonly ISysDeptRoleService _deptRoleService;
        //IRedisCachingProvider
        public HomeController(ISysUserService userService,
            ISysUserDeptService userDeptService,
            ISysUserRoleService userRoleService,
            ISysRoleModuleService roleModuleService,
            ISysModuleService moduleService,
            ISysDeptRoleService deptRoleService,
            IEasyCachingProviderFactory factory) : base(factory)
        {
            _userService = userService;
            _userDeptService = userDeptService;
            _userRoleService = userRoleService;
            _roleModuleService = roleModuleService;
            _moduleService = moduleService;
            _deptRoleService = deptRoleService;
        }

        [AllowAnonymous]
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
            UserInfo loginUserRes = new UserInfo
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Avator = user.Avator,
                Mobile = user.Mobile,
                Email = user.Email,
                //Depts = new List<long>(),
                //Roles = new List<long>(),
                //GrantedModules = new List<TreeNode>()
            };
            var depts = await GetUserDepts(user.Id);
            var roles = await GetUserRoles(user.Id, depts);
            var moduleList = await GetUserModules(roles);

            var treeNode = new GrantedModule();
            TreeHelper.ListToTree(treeNode, moduleList);
            //loginUserRes.GrantedModules = treeNode.Children;

            #region 设置用户基本信息
            _cache.Set($"User${user.Id}", loginUserRes, TimeSpan.FromDays(1));
            #endregion

            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "UserId", user.Id.ToString() },
                { "UserName", user.UserName }
            };
            var token = TokenHelper.GenerateToken(dic, 60);
            var res = new
            {
                token = token,
                userInfo = loginUserRes,
                moduleList
            };
            return Success(res);
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
            var temp = from relation in _roleModuleService.GetQueryable(m => roles.Contains(m.RoleId)).Select(m => new { m.ModuleId, m.FuncPermission })
                       join module in _moduleService.GetQueryable() on relation.ModuleId equals module.Id
                       select new SysModuleRes
                       {
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

    }
}