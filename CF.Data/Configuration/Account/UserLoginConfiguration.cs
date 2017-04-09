using CF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data.Configuration
{
    internal class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
    {
        internal UserLoginConfiguration()
        {
            ToTable("UserLogin", DbSchemaName.ACCOUNT);
            //HasKey(e => new { e.UserId, e.LoginProvider, e.ProviderKey });
        }
    }
}
