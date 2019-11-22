using LeoProject.Common.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Model
{
    /// <summary>
    /// 公司部门
    /// </summary>
    [Table("SysDepartment", Schema = "sys")]
    public class SysDepartment:EntityTree
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = "";
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; } = 0;
        /// <summary>
        /// 部门备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; } = "";
    }
}
