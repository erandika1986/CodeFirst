using CF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data.Configuration
{
    internal class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
    {
        internal UserClaimConfiguration()
        {
            ToTable("UserClaim", DbSchemaName.ACCOUNT);
        }
    }
}
