using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data.Repository
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        internal Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; private set; }
        protected DbSet<T> DbSet { get; private set; }


        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetById(long id)
        {
            return DbSet.Find(id);
        }

        public virtual Task<T> GetByIdAsync(long id)
        {
            return DbSet.FindAsync(id);
        }

        public virtual T GetById(string id)
        {
            return DbSet.Find(id);
        }

        public virtual Task<T> GetByIdAsync(string id)
        {
            return DbSet.FindAsync(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void DeAttached(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Detached;
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(long id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }

        public virtual async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(string id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }
    }
}
