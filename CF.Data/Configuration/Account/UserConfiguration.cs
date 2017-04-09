using CF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data.Configuration
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        internal UserConfiguration()
        {
            ToTable("User", DbSchemaName.ACCOUNT);

            HasOptional<User>(m => m.CreatedBy).WithMany(t => t.CreatedUsers).HasForeignKey(m => m.CreatedById).WillCascadeOnDelete(false);
            HasOptional<User>(m => m.UpdatedBy).WithMany(t => t.UpdatedUsers).HasForeignKey(m => m.UpdatedById).WillCascadeOnDelete(false);

            Property(m => m.UserName).HasMaxLength(DbFieldLength.USER_NAME_MAX_LENGHT).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute() { IsUnique = true } }));
            Property(m => m.PasswordHash).HasMaxLength(DbFieldLength.PASSWORD_HASH_MAX_LENGTH);
            Property(m => m.SecurityStamp).HasMaxLength(DbFieldLength.SECURUTY_STAMP_MAX_LENGHTH);
        }
    }
}
