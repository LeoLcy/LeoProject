using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeoProject.Infrastructure.Tree
{
    public static class TreeHelper
    {
        /// <summary>
        /// 生成树操作
        /// </summary>
        /// <typeparam name="TTreeNode">继承TreeNode的对象</typeparam>
        /// <typeparam name="TTreeItem">继承TreeItem的对象</typeparam>
        /// <param name="rootNode">树的根节点</param>
        /// <param name="list">组成树的列表</param>
        /// <param name="parentId">父Id</param>
        public static void ListToTree<TTreeNode, TTreeItem>(TTreeNode rootNode, List<TTreeItem> list, long parentId = 0)
            where TTreeNode : TreeNode
            where TTreeItem : TreeItem
        {
            var listParent = list.Where(m => m.ParentId == parentId).ToList();
            if (listParent == null || listParent.Count == 0)
            {
                return;
            }
            var tnType = rootNode.GetType();
            foreach (var item in listParent)
            {
                var listChild = list.Where(m => m.ParentId == item.Id).ToList();

                var tn = Activator.CreateInstance(tnType) as TTreeNode;

                var propertyInfos = item.GetType().GetProperties();
                foreach (var property in propertyInfos)
                {
                    var tnProperty = tnType.GetProperty(property.Name);

                    if (tnProperty != null)
                    {
                        tnProperty.SetValue(tn, property.GetValue(item));
                    }
                }
                if (listChild != null && listChild.Count > 0)
                {
                    ListToTree(tn, list, item.Id);
                }
                if (rootNode.Children == null)
                {
                    rootNode.Children = new List<TreeNode>();
                }
                rootNode.Children.Add(tn);
            }
        }

    }
}
