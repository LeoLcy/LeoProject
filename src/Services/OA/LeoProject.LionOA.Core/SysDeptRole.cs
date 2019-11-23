using LeoProject.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Core
{
    /// <summary>
    /// 部门角色关联表
    /// </summary>
    [Table("SysDeptRole", Schema = "sys")]
    public class SysDeptRole : Entity
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// RoleId
        /// </summary>
        public long RoleId { get; set; }
    }
}
