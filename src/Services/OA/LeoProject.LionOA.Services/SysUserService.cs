using LeoProject.Infrastructure.Database;
using LeoProject.Infrastructure.Database.Data;
using LeoProject.Infrastructure.Services;
using LeoProject.LionOA.Core;
using LeoProject.LionOA.EntityFrameworkCore.EntityFrameworkCore;
using LeoProject.LionOA.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeoProject.LionOA.Services
{
    public class SysUserService:BaseService<SysUser>,ISysUserService
    {
        private OADbContext db;
        public SysUserService(OADbContext  _db):base(_db)
        {
            db = _db;
        }

        //public async Task<SysUser> Login(string userName,string password)
        //{
        //    var user = await FindFirstOrDefaultAsync(m => m.UserName == userName && m.Password == password);
        //    if (user != null)
        //    {
        //        return user;
        //    }
        //    return null;
        //}
    }
}
