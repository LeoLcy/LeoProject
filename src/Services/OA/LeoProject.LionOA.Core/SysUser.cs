﻿using LeoProject.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Core
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("SysUser",Schema ="sys")]
    public class SysUser:Entity
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        //[MaxLength(100)]
        //public string Account { get; set; } = "";
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50),Required]
        public string UserName { get; set; } = "";
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(50), Required]
        public string Password { get; set; } = "";
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50), Required]
        public string RealName { get; set; } = "";
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50), Required]
        public string NickName { get; set; } = "";
        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(200)]
        public string Avator { get; set; } = "";
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(20), Required]
        public string Mobile { get; set; } = "";
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50), Required]
        public string Email { get; set; } = "";
        /// <summary>
        /// 角色备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; } = "";
    }
}
