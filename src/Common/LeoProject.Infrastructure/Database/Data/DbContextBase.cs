using LeoProject.Infrastructure.Database.Extensions;
using LeoProject.Infrastructure.Database.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;


namespace LeoProject.Infrastructure.Database.Data
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
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

        /// <summary>
        /// 记录数据库日志
        /// </summary>
        public static readonly LoggerFactory MyLoggerFactory
        = new LoggerFactory(new[] 
        {
            //(ILoggerProvider)new EFLoggerProvider(),
            new ConsoleLoggerProvider((category, level)
            => category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information, true),
        });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Warning: Do not create a new ILoggerFactory instance each time
            //var loggerFactory = new LoggerFactory();
            //loggerFactory.AddProvider(new EFLoggerProvider());
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
