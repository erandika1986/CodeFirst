using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Model
{
    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NICNo { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long? UpdatedById { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }

        #region  Navigation Properties for Account Schema

        public virtual ICollection<Role> CreatedRoles { get; set; }
        public virtual ICollection<Role> UpdatedRoles { get; set; }

        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> UpdatedUsers { get; set; }

        public virtual ICollection<UserRole> CreatedUserRoles { get; set; }
        public virtual ICollection<UserRole> UpdatedUserRoles { get; set; }

        #endregion
    }
}
