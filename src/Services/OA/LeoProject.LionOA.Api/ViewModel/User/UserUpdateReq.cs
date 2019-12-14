using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Role
{
    /// <summary>
    /// 更新用户
    /// </summary>
    public class UserUpdateReq
    {
        public long Id { get; set; } = 0;
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
