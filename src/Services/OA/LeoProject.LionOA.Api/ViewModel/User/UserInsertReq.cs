using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Role
{
    public class UserInsertReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50), Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; } = "";
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(50), Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; } = "";
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50), Required(ErrorMessage = "真实姓名不能为空")]
        public string RealName { get; set; } = "";
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50), Required(ErrorMessage = "昵称不能为空")]
        public string NickName { get; set; } = "";
        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(200)]
        public string Avator { get; set; } = "";
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(20), Required(ErrorMessage = "手机号不能为空")]
        public string Mobile { get; set; } = "";
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50), Required(ErrorMessage = "邮箱不能为空")]
        public string Email { get; set; } = "";
        /// <summary>
        /// 角色备注
        /// </summary>
        [MaxLength(200, ErrorMessage = "备注内容最大长度为200")]
        public string Remark { get; set; } = "";
    }
}
