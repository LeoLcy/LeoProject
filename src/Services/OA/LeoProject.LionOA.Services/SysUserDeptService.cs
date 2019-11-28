using LeoProject.Infrastructure.Services;
using LeoProject.LionOA.Core;
using LeoProject.LionOA.EntityFrameworkCore.EntityFrameworkCore;
using LeoProject.LionOA.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.LionOA.Services
{
    public class SysUserDeptService : BaseService<SysUserDept>, ISysUserDeptService
    {
        private OADbContext db;
        public SysUserDeptService(OADbContext _db) : base(_db)
        {
            db = _db;
        }
    }
}
