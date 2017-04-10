using CF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    public interface ICFUow : IDisposable
    {
        void Commit();
        Task<int> CommitAsync();

        void BeginTransaction();
        void EndTransaction();
        void RolebackTransaction();
        void EnableBulkInsert();
        void DisableBulkInsert();

        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IUserRoleRepository UserRoles { get; }
    }
}
