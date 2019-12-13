using LeoProject.Infrastructure.Controllers.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.Infrastructure.Tree
{
    /// <summary>
    /// 树形结构节点类
    /// </summary>
    public class TreeNode : ITreeItem 
    {
        public long Id { get; set; } = 0;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 父Id
        /// </summary>
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 父名字
        /// </summary>
        public string ParentName { get; set; } = "";
        /// <summary>
        /// 父子路径，以“|”分开
        /// </summary>
        public string TreePath { get; set; } = "";

        public List<TreeNode> Children { get; set; } //= new List<ITreeNode>();
    }
    //public interface ITreeNode: ITreeItem
    //{
    //    List<ITreeNode> Children { get; set; }
    //}
    /// <summary>
    /// 生成树形结构的列表项
    /// </summary>
    public class TreeItem : ITreeItem
    {
        public long Id { get; set; } = 0;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 父Id
        /// </summary>
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 父名字
        /// </summary>
        public string ParentName { get; set; } = "";
        /// <summary>
        /// 父子路径，以“|”分开
        /// </summary>
        public string TreePath { get; set; } = "";
    }
    /// <summary>
    /// 组建树形结构的列表数据项
    /// </summary>
    public interface ITreeItem
    {
        long Id { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        long ParentId { get; set; }
        /// <summary>
        /// 父名字
        /// </summary>
        string ParentName { get; set; }
        /// <summary>
        /// 父子路径，以“|”分开
        /// </summary>
        string TreePath { get; set; }
    }
}
