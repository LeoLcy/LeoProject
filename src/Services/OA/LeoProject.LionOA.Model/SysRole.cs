using LeoProject.Common.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Model
{
    /// <summary>
    /// 系统角色表
    /// </summary>
    [Table("SysRole", Schema = "sys")]
    public class SysRole:Entity
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = "";
        /// <summary>
        /// 角色备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; } = "";
    }
}
