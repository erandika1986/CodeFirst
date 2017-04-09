using CF.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    public class CustomRoleStore : RoleStore<Role, long, UserRole>
    {
        public CustomRoleStore(MyDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
