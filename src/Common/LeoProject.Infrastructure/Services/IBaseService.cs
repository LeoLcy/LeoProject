using LeoProject.Infrastructure.Database;
using LeoProject.Infrastructure.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.Infrastructure.Services
{
    public interface IBaseService<T>:IRepository<T> where T :IEntity<long>
    {

    }
}
