using LeoProject.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Core
{
    /// <summary>
    /// 用户部门关联表
    /// 
    /// </summary>
    [Table("SysRoleModule", Schema = "sys")]
    public class SysRoleModule : Entity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// ModuleId
        /// </summary>
        public long ModuleId { get; set; }
        /// <summary>
        /// 分配带角色下页面的功能权限
        /// </summary>
        public string FuncPermission { get; set; }
    }
}
