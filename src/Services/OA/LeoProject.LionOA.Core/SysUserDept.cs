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
    [Table("SysUserDept", Schema = "sys")]
    public class SysUserDept:Entity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public long DeptId { get; set; }
    }
}
