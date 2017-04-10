using CF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data.Repository
{
    internal class RoleRepository : Repository<Role>, IRoleRepository
    {

        internal RoleRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
