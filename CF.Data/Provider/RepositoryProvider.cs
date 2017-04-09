using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    internal class RepositoryProvider : IRepositoryProvider
    {

        #region Member Variables

        private readonly RepositoryFactories repositoryFactories;
        private readonly Dictionary<Type, object> repositories;
        private readonly DbContext dbContext;

        #endregion

        #region Constructor

        internal RepositoryProvider(DbContext dbContext, RepositoryFactories repositoryFactories)
        {
            this.dbContext = dbContext;
            this.repositoryFactories = repositoryFactories;
            this.repositories = new Dictionary<Type, object>();
        }

        #endregion

        #region Public Methods

        public IRepository<T> GetRepositoryByEntity<T>() where T : class
        {
            return GetRepository<IRepository<T>>(repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        public T GetRepositoryByRepository<T>(Func<DbContext, object> factory = null) where T : class
        {
            return GetRepository<T>();
        }

        #endregion

        #region Private Methods

        private T GetRepository<T>(Func<DbContext, object> factory = null) where T : class
        {
            object repository;
            if (repositories.TryGetValue(typeof(T), out repository))
            {
                return (T)repository;
            }
            else
            {
                factory = factory ?? repositoryFactories.GetRepositoryFactory<T>();

                if (factory == null)
                {
                    throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
                }

                repository = factory(dbContext);
                repositories[typeof(T)] = repository;
                return (T)repository;
            }
        }

        #endregion

    }
}
