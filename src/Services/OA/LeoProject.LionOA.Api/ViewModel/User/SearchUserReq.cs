using LeoProject.Infrastructure.Controllers.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Role
{
    public class SearchUserReq : PageRequestBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "";
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = "";
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = "";
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; } = "";
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = "";
    }
}
