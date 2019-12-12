using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Request.Role
{
    /// <summary>
    /// 角色分配模块请求参数
    /// </summary>
    public class RoleAddModuleReq
    {
        public long RoleId { get; set; } = 0;
        /// <summary>
        /// 为角色分配的模块
        /// </summary>
        public List<ModuleItemReq> Modules { get; set; }
    }
    /// <summary>
    /// 角色分配的模块项
    /// </summary>
    public class ModuleItemReq
    {
        public long ModuleId { get; set; } = 0;
        /// <summary>
        /// 操作权限，以“,”隔开
        /// </summary>
        public string FuncPermission { get; set; }
    }
}
