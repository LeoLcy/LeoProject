using LeoProject.Infrastructure;
using LeoProject.Infrastructure.Helpers;
using LeoProject.LionOA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeoProject.LionOA.EntityFrameworkCore.EntityFrameworkCore.Seed
{
   public static class SeedHelper
    {
        public static void SeedHostDb(OADbContext db)
        {
            if (db.Database.EnsureCreated())
            {
                //create seed data
                var user = db.SysUsers.Where(m => m.UserName == "admin").FirstOrDefault();
                if (user == null)
                {
                    var sysUser = new SysUser
                    {
                        Password = Md5Helper.Encrypt("123456"),
                        UserName = "admin",
                        NickName = "leo"
                    };
                    db.SysUsers.Add(sysUser);
                    db.SaveChanges();
                }
            }
        }
    }
}
