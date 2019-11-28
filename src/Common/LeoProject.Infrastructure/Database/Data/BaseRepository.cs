using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LeoProject.Infrastructure.Database.Data
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity<long>
    {
        #region 初始化对象
        BaseDbContext  db;
        public BaseRepository(BaseDbContext  _db)
        {
            db = _db;
        }
        private DbSet<TEntity> DBSet
        {
            get { return db.Set<TEntity>(); }
        }
        #endregion
        #region Async
        #region select
        public async Task<TEntity> GetByIdAsync(long primaryId)
        {
            var entity = await DBSet.Where(m => m.Id == primaryId).FirstOrDefaultAsync();
            return entity;
        }
        public async Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> exp)
        {
            var entity = await DBSet.Where(exp).FirstOrDefaultAsync();
            return entity;
        }
        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await DBSet.SingleAsync(exp);
        }
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await DBSet.AnyAsync(exp);
        }
        #endregion
        #region count
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            var count =await DBSet.Where(expression).CountAsync();
            return count;
        }
        #endregion
        #region insert, update, delete
        public async Task<int> InsertAsync(TEntity entity)
        {
            DBSet.Add(entity);
            return await SaveAsync();
        }
        public async Task<int> InsertRangeAsync(IEnumerable<TEntity> entityList)
        {
            DBSet.AddRange(entityList);
            return await SaveAsync();
        }
        public async Task<int> UpdateAsync(TEntity entity)
        {
            DBSet.Update(entity);
            return await SaveAsync();
        }
        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entityList)
        {
            DBSet.UpdateRange(entityList);
            return await SaveAsync();
        }
        public async Task<int> DeleteByIdAsync(long primaryId)
        {
            var entity = DBSet.Where(m => m.Id == primaryId).FirstOrDefault();
            DBSet.Remove(entity);
            return await SaveAsync();
        }
        public async Task<int> DeleteAsync(TEntity entity)
        {
            DBSet.Remove(entity);

            return await SaveAsync();
        }
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> exp)
        {
            var entities = DBSet.Where(exp);
            DBSet.RemoveRange(entities);

            return await SaveAsync();
        }
        public async Task<int> DeleteRangeAsync(IEnumerable<long> ids)
        {
            var entities = DBSet.Where(m => ids.Contains(m.Id));
            DBSet.RemoveRange(entities);
            return await SaveAsync();
        }
        #endregion
        #endregion
        #region select
        public TEntity GetById(long primaryId)
        {
            var entity = DBSet.Where(m => m.Id == primaryId).FirstOrDefault();
            return entity;
        }
        public TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> exp)
        {
            var entity = DBSet.Where(exp).FirstOrDefault();
            return entity;
        }
        public TEntity FindSingle(Expression<Func<TEntity, bool>> exp)
        {
            return DBSet.Single(exp);
        }
        public bool IsExist(Expression<Func<TEntity, bool>> exp)
        {
            return DBSet.Any(exp);
        }
        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> exp = null)
        {
            return Filter(exp);
        }
        public IQueryable<TEntity> GetPagedList(int pageindex = 1, int pagesize = 10,
            Expression<Func<TEntity, bool>> exp = null)
        {
            if (pageindex < 1) pageindex = 1;

            return Filter(exp).OrderByDescending(m => m.Id).Skip(pagesize * (pageindex - 1)).Take(pagesize);
        }
        #endregion
        #region count
        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            var count = DBSet.Where(expression).Count();
            return count;
        }
        #endregion
        #region insert, update, delete
        public int Insert(TEntity entity)
        {
            DBSet.Add(entity);
            return Save();
        }
        public int InsertRange(IEnumerable<TEntity> entityList)
        {
            DBSet.AddRange(entityList);
            return Save();
        }
        public int Update(TEntity entity)
        {
            DBSet.Update(entity);
            return Save();
        }
        public int UpdateRange(IEnumerable<TEntity> entityList)
        {
            DBSet.UpdateRange(entityList);
            return Save();
        }
        public int DeleteById(long primaryId)
        {
            var entity = DBSet.Where(m => m.Id == primaryId).FirstOrDefault();
            DBSet.Remove(entity);
            return Save();
        }
        public int Delete(TEntity entity)
        {
            DBSet.Remove(entity);

            return Save();
        }
        public int Delete(Expression<Func<TEntity, bool>> exp)
        {
            var entities = DBSet.Where(exp);
            DBSet.RemoveRange(entities);

            return Save();
        }
        public int DeleteRange(IEnumerable<long> ids)
        {
            var entities = DBSet.Where(m => ids.Contains(m.Id));
            DBSet.RemoveRange(entities);
            return Save();
        }
        #endregion

        public int ExecuteSql(string sql, params object[] parameters)
        {
            return db.Database.ExecuteSqlCommand(sql, parameters);
        }
        #region 私有方法

        private IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> exp)
        {
            var dbSet = DBSet.AsNoTracking().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }
        private int Save()
        {
            return db.SaveChanges();
        }
        private async Task<int> SaveAsync()
        {
            return await db.SaveChangesAsync();
        }
        #endregion
    }
}
