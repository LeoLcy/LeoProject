using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel
{
    /// <summary>
    /// 当前登录用户的信息
    /// </summary>
    public class UserInfo
    {

        public long UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "";
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = "";
        /// <summary>
        /// 头像地址
        /// </summary>
        public string Avator { get; set; } = "";
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; } = "";
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = "";
        /// <summary>
        /// 用户角色
        /// </summary>
        //public List<long> Roles { get; set; }
        ///// <summary>
        ///// 用户部门
        ///// </summary>
        //public List<long> Depts { get; set; }
        ///// <summary>
        ///// 用户权限树形结构
        ///// </summary>
        //public List<TreeNode> GrantedModules { get; set; }
        ///// <summary>
        ///// 权限列表
        ///// </summary>
        //public List<SysModuleRes> ModuleList { get; set; }
    }
}
