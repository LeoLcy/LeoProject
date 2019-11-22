using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeoProject.LionWorkflow.Model
{
    /// <summary>
    /// 工作流分类表 类别表
    /// </summary>
    public class WfWorkflowCategory: EntityTree
    {
        /// <summary>
        /// 名字
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = "";
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
