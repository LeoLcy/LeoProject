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

 

    }
}