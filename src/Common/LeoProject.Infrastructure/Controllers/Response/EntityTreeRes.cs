using LeoProject.Infrastructure.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.Infrastructure.Controllers.Response
{
    public class EntityTreeRes: ITreeItem
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
}
