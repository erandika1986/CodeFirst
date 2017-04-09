using CF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data.Configuration
{
    internal class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        internal RoleConfiguration()
        {
            ToTable("Role", DbSchemaName.ACCOUNT);

            HasRequired<User>(m => m.CreatedBy).WithMany(t => t.CreatedRoles).HasForeignKey(m => m.CreatedById).WillCascadeOnDelete(false);
            HasRequired<User>(m => m.UpdatedBy).WithMany(t => t.UpdatedRoles).HasForeignKey(m => m.UpdatedById).WillCascadeOnDelete(false);

            Property(m => m.Name).HasMaxLength(DbFieldLength.ROLE_NAME_MAX_LENGHTH);
            Property(m => m.Description).HasMaxLength(DbFieldLength.ROLE_DESCRIPTION_MAX_LENGHTH);
        }
    }
}
