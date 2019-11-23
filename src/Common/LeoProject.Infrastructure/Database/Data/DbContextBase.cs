using LeoProject.Infrastructure.Database.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace LeoProject.Infrastructure.Database.Data
{
   public  class DbContextBase: DbContext
    {
        public DbContextBase(DbContextOptions options) : base(options)
        {
        }
        /// <summary>
        /// model构造器  创建实体映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IIsDelete).IsAssignableFrom(type.ClrType))
                {
                    modelBuilder.Entity(type.ClrType).AddQueryFilter<IIsDelete>(t => t.IsDelete == false);
                }
                if (typeof(IIsEnable).IsAssignableFrom(type.ClrType))
                {
                    modelBuilder.Entity(type.ClrType).AddQueryFilter<IIsEnable>(t => t.IsEnable == true);
                }
            }
        }
    }
}
