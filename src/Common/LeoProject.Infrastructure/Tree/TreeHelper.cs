using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeoProject.Infrastructure.Tree
{
    public static class TreeHelper
    {
        /// <summary>
        /// 生成树
        /// </summary>
        /// <param name="rootNode">继承TreeNode的对象</param>
        /// <param name="list"></param>
        /// <param name="parentId"></param>
        public static void ListToTree(ITreeNode rootNode, List<ITreeItem> list ,long parentId=0)
        {
            var listParent = list.Where(m => m.ParentId == parentId).ToList();

            foreach (var item in listParent)
            {
                var listChild = list.Where(m => m.ParentId == item.Id).ToList();

                var tnType = rootNode.GetType();
                var tn = Activator.CreateInstance(tnType) as ITreeNode;

                var propertyInfos = item.GetType().GetProperties();
                foreach (var property in propertyInfos)
                {
                    var tnProperty = tnType.GetProperty(property.Name);
                    if (tnProperty != null)
                    {
                        tnProperty.SetValue(tn, property.GetValue(item));
                    }
                }
                //tn.Id = item.Id;
                //tn.Name = item.Name;
                //tn.ParentId = item.ParentId;
                //tn.ParentName = item.ParentName;
                //tn.TreePath = item.TreePath;
                
                if (listChild != null)
                {
                    ListToTree(tn, list, item.Id);
                }
                rootNode.Children.Add(tn);
            }
        }
    }
}
