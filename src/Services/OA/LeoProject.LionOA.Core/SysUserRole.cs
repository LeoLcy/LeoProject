using LeoProject.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Core
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    [Table("SysUserRole", Schema = "sys")]
    public class SysUserRole:Entity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }
    }
}
