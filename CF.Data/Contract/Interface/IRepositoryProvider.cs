using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    internal interface IRepositoryProvider
    {
        IRepository<T> GetRepositoryByEntity<T>() where T : class;
        T GetRepositoryByRepository<T>(Func<DbContext, object> factory = null) where T : class;
    }
}
