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
        DbContextBase db;
        public BaseRepository(DbContextBase _db)
        {
            db = _db;
        }
        private DbSet<TEntity> DBSet
        {
            get { return db.Set<TEntity>(); }
        }
        #region select
        public TEntity GetById(long primaryId)
        {
            var entity = DBSet.Where(m => m.Id == primaryId).FirstOrDefault();
            return entity;
        }
        public TEntity FindSingle(Expression<Func<TEntity, bool>> exp = null)
        {
            return DBSet.Single(exp);
        }
        public bool IsExist(Expression<Func<TEntity, bool>> exp)
        {
            return DBSet.Any(exp);
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp = null)
        {
            return Filter(exp);
        }
        public IQueryable<TEntity> Find(int pageindex = 1, int pagesize = 10,
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
