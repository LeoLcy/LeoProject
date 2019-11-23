using LeoProject.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeoProject.LionOA.Core
{
    /// <summary>
    /// 用户信息扩展表
    /// </summary>
    [Table("SysUserExtra", Schema ="sys")]
    public class SysUserExtra : Entity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; } = 0;
        /// <summary>
        /// 微信的OpenId
        /// </summary>
        [MaxLength(50)]
        public string OpenId { get; set; } = "";
        /// <summary>
        /// 微信的UnionId
        /// </summary>
        [MaxLength(50)]
        public string UnionId { get; set; } = "";
    }
}
