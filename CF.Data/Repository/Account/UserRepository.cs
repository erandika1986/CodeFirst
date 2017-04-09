using CF.Model;

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CF.Data.Repository
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        internal UserRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public User GetByUserName(string userName)
        {
            return GetAll().Where(x => x.UserName.Replace(" ", string.Empty).Trim().ToLower() == userName.Replace(" ", string.Empty).Trim().ToLower()).FirstOrDefault();
        }

        public User GetByFirstName(string firstName)
        {
            return GetAll().Where(x => x.IsActive == true && x.FirstName.Replace(" ", string.Empty).Trim().ToLower() == firstName.Replace(" ", string.Empty).Trim().ToLower()).FirstOrDefault();
        }

        //public List<User> GetAllActiveUsersByNameRoleId(string Name, long RoleId)
        //{
        //    var lstuser = new List<User>();
        //    if (RoleId == 0 && Name != "")
        //    {
        //        lstuser = GetAll().Where(x => x.FullName.Replace(" ", string.Empty).Trim().ToLower() == Name.Replace(" ", string.Empty).Trim().ToLower()).ToList();
        //    }
        //    else if (Name == "" && RoleId != 0)
        //    {
        //        lstuser = GetAll().Include("UserRoles").Where(x => x.Roles.Select(s => s.RoleId).Contains(RoleId)).ToList();
        //    }
        //    else if (Name != "" && RoleId != 0)
        //    {
        //        lstuser = GetAll().Include("UserRoles").Where(x => x.Roles.Select(s => s.RoleId).Contains(RoleId)
        //            && (x.FullName.Replace(" ", string.Empty).Trim().ToLower() == Name.Replace(" ", string.Empty).Trim().ToLower())).ToList();
        //    }

        //    return lstuser;

        //}

        //public User FindUser(string userName, string passWord)
        //{
        //    PasswordVerificationResult result = PasswordVerificationResult.Failed;
        //    try
        //    {
        //        var passwordHasher = new SLIAPasswordHasher();
        //        var user = GetAll().Where(x => x.UserName.Replace(" ", string.Empty).Trim().ToLower() == userName.Replace(" ", string.Empty).Trim().ToLower()).FirstOrDefault();
        //        if (user != null)
        //        {
        //            result = passwordHasher.VerifyHashedPassword(user.PasswordHash, passWord);
        //        }

        //        if (result == PasswordVerificationResult.Success)
        //        {
        //            return user;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


    }
}
