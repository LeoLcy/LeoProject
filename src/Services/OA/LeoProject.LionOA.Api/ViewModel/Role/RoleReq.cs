using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Api.ViewModel.Role
{
    public class RoleReq
    {
        /// <summary>
        /// Id值，如果有值>0则为更新，其他为新增
        /// </summary>
        public long Id { get; set; } = 0;
        /// <summary>
        /// 角色名
        /// </summary>
        [MaxLength(50,ErrorMessage ="角色名最大长度为50"), Required(ErrorMessage ="角色名称不能为空")]
        public string Name { get; set; } = "";
        /// <summary>
        /// 角色备注
        /// </summary>
        [MaxLength(200, ErrorMessage = "备注内容最大长度为200")]
        public string Remark { get; set; } = "";
    }
}
