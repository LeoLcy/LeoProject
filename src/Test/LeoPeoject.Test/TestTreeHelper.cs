using LeoProject.Infrastructure.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LeoPeoject.Test
{
    [TestClass]
    public class TestTreeHelper
    {
        [TestMethod]
        public void TestListToTree()
        {
            TreeNode rootNode = new TreeNode();
            var list = GetTreeItems();
            TreeHelper.ListToTree(rootNode, list);
            var str = JsonConvert.SerializeObject(rootNode);


            Assert.IsTrue(rootNode.Children.Count == 2);
        }

        [TestMethod]
        public void TestListToTree1()
        {
            TreeNodeExt rootNode = new TreeNodeExt();
            var list = GetTreeItemExts();
            TreeHelper.ListToTree(rootNode, list);
            List<TreeNode> nodes= rootNode.Children;
            var str = JsonConvert.SerializeObject(nodes);

            
            Assert.IsTrue(rootNode.Children.Count == 2);
        }

        private List<TreeItem> GetTreeItems()
        {
            var list = new List<TreeItem>()
            {
                new TreeItem
                {
                    Id = 1,
                    Name="1",
                    ParentId=0,
                },
                new TreeItem
                {
                    Id = 2,
                    Name="2",
                    ParentId=0,
                },
                new TreeItem
                {
                    Id = 3,
                    Name="1-1",
                    ParentId=1,
                },new TreeItem
                {
                    Id = 4,
                    Name="1-2",
                    ParentId=1,
                },new TreeItem
                {
                    Id = 5,
                    Name="2-1",
                    ParentId=2,
                },
            };

            return list;
        }
        private List<TreeItemExt> GetTreeItemExts()
        {
            var list = new List<TreeItemExt>()
            {
                new TreeItemExt
                {
                    Id = 1,
                    Name="1",
                    ParentId=0,
                    Ext="1"
                },
                new TreeItemExt
                {
                    Id = 2,
                    Name="2",
                    ParentId=0,
                     Ext="2",
                },
                new TreeItemExt
                {
                    Id = 3,
                    Name="1-1",
                    ParentId=1,
                     Ext="2",
                },new TreeItemExt
                {
                    Id = 4,
                    Name="1-2",
                    ParentId=1,
                     Ext="2",
                },new TreeItemExt
                {
                    Id = 5,
                    Name="2-1",
                    ParentId=2,
                     Ext="2",
                },
            };

            return list;
        }
    }
    public class TreeNodeExt : TreeNode
    {
        public string Ext { get; set; }
    }
    public class TreeItemExt : TreeItem
    {
        public string Ext { get; set; }
    }
}
