using CF.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    internal class RepositoryFactories
    {
        #region Member Variables

        private readonly IDictionary<Type, Func<DbContext, object>> repositoryFactories;

        #endregion

        #region Constructor

        internal RepositoryFactories()
        {
            this.repositoryFactories = GetCustomFactories();
        }

        #endregion

        #region Private Methods

        private IDictionary<Type, Func<DbContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
            {
                { typeof(IUserRepository), (dbContext)=> new UserRepository(dbContext)},
                { typeof(IUserRoleRepository),(dbContext)=>new UserRoleRepository(dbContext)},
                { typeof(IRoleRepository),(dbContext)=>new RoleRepository(dbContext)}
            };
        }

        private Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new Repository<T>(dbContext);
        }

        #endregion

        #region Public Methods

        public Func<DbContext, object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> factory;
            repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        #endregion
    }
}
