using LeoProject.Infrastructure.Database;
using LeoProject.Infrastructure.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.Infrastructure.Services
{
    public class BaseService<T>:BaseRepository<T>, IBaseService<T> where T : Entity<long>
    {
        public BaseService(BaseDbContext  _db):base(_db)
        {
        }
    }
}
