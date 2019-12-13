using LeoProject.Infrastructure.Controllers.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Module
{
    public class SearchModuleReq : PageRequestBase
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentId { get; set; } = -1;
    }
}
