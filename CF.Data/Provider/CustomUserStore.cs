using CF.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    public class CustomUserStore : UserStore<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        public CustomUserStore(MyDbContext dbContext)
            : base(dbContext)
        {

        }


    }
}
