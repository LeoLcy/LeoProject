using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Module
{
    public class ModuleReq
    {
        /// <summary>
        /// Id值，如果有值>0则为更新，其他为新增
        /// </summary>
        public long Id { get; set; } = 0;
        /// <summary>
        /// 资源名称
        /// </summary>
        [MaxLength(50,ErrorMessage = "资源名称名称最长50个字"),Required(ErrorMessage = "资源名称不能为空")]
        public string Name { get; set; } = "";
        /// <summary>
        /// 父Id， 默认为0
        /// </summary>
        [Required]
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 父名字， ParentId为0的话，ParentName为空
        /// </summary>
        [MaxLength(50)]
        public string ParentName { get; set; } = "";
        /// <summary>
        /// 父子路径，以“|”分开
        /// </summary>
        //[MaxLength(200)]
        //public string TreePath { get; set; } = "";
        /// <summary>
        /// 主页面URL
        /// </summary>
        [MaxLength(255),Required(ErrorMessage = "主页面Url不能为空")]
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
        [MaxLength(255), Required(ErrorMessage = "前端组件(.vue)不能为空")]
        public string Component { get; set; } = "";
        /// <summary>
        /// 功能权限 页面包含的操作按钮权限
        /// add(新增)update(修改)delete(删除)export(导出)
        /// 也可以是针对的接口
        /// </summary>
        [MaxLength(255)]
        public string FuncPermission { get; set; } = "";
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
