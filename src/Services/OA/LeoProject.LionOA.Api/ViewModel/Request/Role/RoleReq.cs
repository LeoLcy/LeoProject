using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Request.Role
{
    public class RoleReq
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [MaxLength(50),Required]
        public string Name { get; set; } = "";
        /// <summary>
        /// 角色备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; } = "";
    }
}
