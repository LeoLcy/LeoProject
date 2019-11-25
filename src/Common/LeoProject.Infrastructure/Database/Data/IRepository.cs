using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LeoProject.Infrastructure.Database.Data
{
    public interface IRepository<T> where T : IEntity<long>
    {
        #region select
        T GetById(long primaryId);
        T FindSingle(Expression<Func<T, bool>> exp = null);
        bool IsExist(Expression<Func<T, bool>> exp);
        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);
        IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "",
            Expression<Func<T, bool>> exp = null);
        #endregion
        #region count
        int Count(Expression<Func<T,bool>> expression);
        #endregion
        #region insert, update, delete
        int Insert(T entity);
        int InsertBatch(IEnumerable<T> entityList);
        int Update(T entity);
        int UpdateBatch(IEnumerable<T> entityList);
        int DeleteById(dynamic primaryId);
        int Delete(T entity);
        int DeleteBatch(IEnumerable<dynamic> ids);
        #endregion

        int ExecuteSql(string sql);
    }
}
