using LeoProject.Infrastructure.Controllers.Response;
using LeoProject.Infrastructure.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Response
{
    public class GrantedModuleTree : TreeNode
    {
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
        /// 菜单图标(可选)
        /// </summary>
        public string Icon { get; set; } = "";
        /// <summary>
        /// 前端组件(.vue)
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 功能权限 主要针对操作的功能权限 以逗号分隔，记录Code
        /// add(新增)update(修改)delete(删除)export(导出)
        /// 也可以是针对的接口
        /// </summary>
        public string FuncPermission { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; } = 0;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";
    }
    /// <summary>
    /// Module返回对象
    /// </summary>
    public class SysModuleRes: TreeItem
    {
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
        /// 菜单图标(可选)
        /// </summary>
        public string Icon { get; set; } = "";
        /// <summary>
        /// 前端组件(.vue)
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 功能权限 主要针对操作的功能权限 以逗号分隔，记录Code
        /// add(新增)update(修改)delete(删除)export(导出)
        /// 也可以是针对的接口
        /// </summary>
        public string FuncPermission { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; } = 0;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";
    }
}
