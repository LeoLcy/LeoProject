using LeoProject.Common.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Model
{
    /// <summary>
    /// 功能模块表
    /// </summary>
    [Table("SysModule", Schema = "sys")]
    public class SysModule : EntityTree
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = "";
        /// <summary>
        /// 主页面URL
        /// </summary>
        public string Url { get; set; } = "";
        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public bool IsLeaf { get; set; } = true;
        /// <summary>
        /// 是否自动展开
        /// </summary>
        public bool IsAutoExpand { get; set; } = true;
        /// <summary>
        /// 节点图标文件名称
        /// </summary>
        public string Icon { get; set; } = "";
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; } = 0;
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; } = "";
    }
}
