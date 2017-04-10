using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CF.Model;
using System.Data.Entity;

namespace CF.Data
{
    public class CFUow : ICFUow
    {
        #region Member Variables

        private IRepositoryProvider repositoryProvider;
        private MyDbContext dbContext;
        private DbContextTransaction transaction;

        #endregion

        #region Constructor

        public CFUow()
        {
            dbContext = new MyDbContext();
            Initialize();
            this.repositoryProvider = new RepositoryProvider(dbContext, new RepositoryFactories());

        }

        #endregion

        #region Initialize

        protected void Initialize()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;
            dbContext.Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion

        public IUserRepository Users { get { return GetRepositoryByRepository<IUserRepository>(); } }
        public IUserRoleRepository UserRoles { get { return GetRepositoryByRepository<IUserRoleRepository>(); } }
        public IRoleRepository Roles { get { return GetRepositoryByRepository<IRoleRepository>(); } }

        #region Public Methods

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            transaction = dbContext.Database.BeginTransaction();
        }

        public void EndTransaction()
        {
            if (transaction != null)
                transaction.Commit();
        }

        public void RolebackTransaction()
        {
            if (transaction != null)
                transaction.Rollback();
        }

        public void EnableBulkInsert()
        {
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            dbContext.Configuration.AutoDetectChangesEnabled = false;
        }

        public void DisableBulkInsert()
        {
            dbContext.Configuration.ValidateOnSaveEnabled = true;
            dbContext.Configuration.AutoDetectChangesEnabled = true;
        }

        #endregion

        #region Private Methods

        private IRepository<T> GetRepositoryByModel<T>() where T : class
        {
            return repositoryProvider.GetRepositoryByEntity<T>();
        }

        private T GetRepositoryByRepository<T>() where T : class
        {
            return repositoryProvider.GetRepositoryByRepository<T>();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        #endregion

    }
}
