﻿using LeoProject.Infrastructure.Database;
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
    public  class SysDepartmentService : BaseService<SysDepartment>, ISysDepartmentService
    {
        private OADbContext db;
        public SysDepartmentService(OADbContext  _db):base(_db)
        {
            db = _db;
        }
    }
}
