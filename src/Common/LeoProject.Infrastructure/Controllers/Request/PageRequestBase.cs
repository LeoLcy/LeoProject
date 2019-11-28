using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.Infrastructure.Controllers.Request
{
    /// <summary>
    /// 请求分页数据基础类
    /// </summary>
    public class PageRequestBase
    {
        /// <summary>
        /// 页面
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
