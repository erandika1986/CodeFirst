using CF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    internal interface IUserRoleRepository : IRepository<UserRole>
    {
        List<UserRole> GetUserRoles();
        List<UserRole> GetUserRolesByUserId(long UserId);
    }
}
