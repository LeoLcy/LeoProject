using LeoProject.Infrastructure.Controllers.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Role
{
    public class SearchRoleReq:PageRequestBase
    {
        public string Name { get; set; }
    }
}
