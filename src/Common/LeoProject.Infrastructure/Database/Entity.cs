using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeoProject.Infrastructure.Database
{
    /// <summary>
    /// 数据库对象公共实体，主键为long
    /// </summary>
    public class Entity : Entity<long>
    {

    }
    public class EntityTree : Entity<long>
    {
        /// <summary>
        /// 父Id
        /// </summary>
        [Required]
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 父名字
        /// </summary>
        [MaxLength(50)]
        public string ParentName { get; set; } = "";
        /// <summary>
        /// 父子路径，以“|”分开
        /// </summary>
        [MaxLength(200)]
        public string TreePath { get; set; } = "";
    }
    public class Entity<TKey> : IEntity<TKey>, IIsDelete, IIsEnable
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public TKey Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public TKey CreateBy { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新人
        /// </summary>
        public TKey UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnable { get; set; } = true;
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; } = false;
    }
    /// <summary>
    /// 基础类型
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        TKey CreateBy { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        TKey UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
    public interface IIsDelete
    {
        bool IsDelete { get; set; }
    }
    public interface IIsEnable
    {
        bool IsEnable { get; set; }
    }
}
