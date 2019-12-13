using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel
{
    /// <summary>
    /// 登录实体对象
    /// </summary>
    public class LoginReq
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}
