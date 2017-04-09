using CF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    interface ISLIAUow : IDisposable
    {
        void Commit();
        Task<int> CommitAsync();

        void BeginTransaction();
        void EndTransaction();
        void RolebackTransaction();
        void EnableBulkInsert();
        void DisableBulkInsert();

        IUserRepository Users { get; }
        IRepository<Role> Roles { get; }
        IUserRoleRepository UserRoles { get; }
    }
}
