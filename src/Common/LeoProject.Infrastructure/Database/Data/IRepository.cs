using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LeoProject.Infrastructure.Database.Data
{
    public interface IRepository<TEntity> where TEntity : IEntity<long>
    {
        #region Async
        #region select
         Task<TEntity> GetByIdAsync(long primaryId);
         Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> exp);
         Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> exp);
         Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp);
        #endregion
        #region count
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        #endregion
        #region insert, update, delete
        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertRangeAsync(IEnumerable<TEntity> entityList);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entityList);
        Task<int> DeleteByIdAsync(long primaryId);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> exp);
        Task<int> DeleteRangeAsync(IEnumerable<long> ids);
        #endregion
        #endregion
        #region select
        TEntity GetById(long primaryId);
        TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> exp);
        TEntity FindSingle(Expression<Func<TEntity, bool>> exp);
        bool IsExist(Expression<Func<TEntity, bool>> exp);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> exp = null);
        IQueryable<TEntity> GetPagedList(int pageindex = 1, int pagesize = 10, 
            Expression<Func<TEntity, bool>> exp = null);
        #endregion
        #region count
        int Count(Expression<Func<TEntity,bool>> expression);
        #endregion
        #region insert, update, delete
        int Insert(TEntity entity);
        int InsertRange(IEnumerable<TEntity> entityList);
        int Update(TEntity entity);
        int UpdateRange(IEnumerable<TEntity> entityList);
        int DeleteById(long primaryId);
        int Delete(TEntity entity);
        int Delete(Expression<Func<TEntity, bool>> exp);
        int DeleteRange(IEnumerable<long> ids);
        #endregion

        int ExecuteSql(string sql, params object[] parameters);
    }
}
