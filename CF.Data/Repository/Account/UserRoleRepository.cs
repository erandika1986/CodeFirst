using CF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data.Repository
{
    internal class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {

        internal UserRoleRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
        public List<UserRole> GetUserRoles()
        {
            return GetAll().Include("Role").ToList();
        }

        public List<UserRole> GetUserRolesByUserId(long UserId)
        {
            return GetAll().Include("Role").Include("User").Where(x => x.UserId == UserId).ToList();
        }

    }
}
