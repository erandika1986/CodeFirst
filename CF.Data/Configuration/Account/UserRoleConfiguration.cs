using CF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data.Configuration
{
    internal class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        internal UserRoleConfiguration()
        {
            ToTable("UserRole", DbSchemaName.ACCOUNT);

            HasKey(u => new { u.UserId, u.RoleId });

            HasRequired<User>(m => m.CreatedBy).WithMany(t => t.CreatedUserRoles).HasForeignKey(m => m.CreatedById).WillCascadeOnDelete(false);
            HasRequired<User>(m => m.UpdatedBy).WithMany(t => t.UpdatedUserRoles).HasForeignKey(m => m.UpdatedById).WillCascadeOnDelete(false);

            HasRequired<User>(m => m.User).WithMany(t => t.Roles).HasForeignKey(m => m.UserId).WillCascadeOnDelete(false);
            HasRequired<Role>(m => m.Role).WithMany(t => t.Users).HasForeignKey(m => m.RoleId).WillCascadeOnDelete(false);
        }
    }
}
