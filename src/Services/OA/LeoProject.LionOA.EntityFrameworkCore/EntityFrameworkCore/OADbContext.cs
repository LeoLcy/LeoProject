﻿using LeoProject.Infrastructure.Database.Data;
using LeoProject.LionOA.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.LionOA.EntityFrameworkCore.EntityFrameworkCore
{
    public class OADbContext : BaseDbContext 
    {
        public OADbContext(DbContextOptions<OADbContext> options)
       : base(options)
        { }

        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
    }
}
