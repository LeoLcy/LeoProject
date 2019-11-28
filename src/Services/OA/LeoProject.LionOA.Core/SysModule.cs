using LeoProject.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Core
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
        [MaxLength(255)]
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
        /// 菜单图标(可选)
        /// </summary>
        [MaxLength(30)]
        public string Icon { get; set; } = "";
        /// <summary>
        /// 前端组件(.vue)
        /// </summary>
        [MaxLength(255)]
        public string Component { get; set; }
        /// <summary>
        /// 功能权限 主要针对操作的功能权限 以逗号分隔，记录Code
        /// add(新增)update(修改)delete(删除)export(导出)
        /// 也可以是针对的接口
        /// </summary>
        [MaxLength(255)]
        public string FuncPermission { get; set; }
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
