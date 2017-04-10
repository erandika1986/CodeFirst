using CF.Data.Configuration;
using CF.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    public class MyDbContext : IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>
    {

        #region Static Constructor

        static MyDbContext()
        {
            Database.SetInitializer(new MyDbInitializer());
        }

        #endregion

        #region Constructor

        public MyDbContext():base("name=MyEntity")
        {
            this.Database.CommandTimeout = 1000;
        }

        #endregion

        #region  Account Entities

        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Ovveride Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Account Entity Configuration
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());
            modelBuilder.Configurations.Add(new UserLoginConfiguration());
        }

        #endregion

        public static MyDbContext Create()
        {
            return new MyDbContext();
        }
    }
}
