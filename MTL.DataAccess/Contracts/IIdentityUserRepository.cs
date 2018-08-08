using System;
using System.Collections.Generic;
using MTL.DataAccess.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Contracts
{
    public interface IIdentityUserRepository
    {
        #region ASYNC
        Task<IdentityResult> CreateIdentityUserAsync(IdentityUser entity, string password);
        Task<IdentityUser> FindByNameAsync(string userName);
        Task<IdentityUser> FindByIdAsync(string id);
        //Task<IdentityUserExtended> FindExtendedByNameAsync(string userName);
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(IdentityUser userToVerify, string password);
        #endregion

        IEnumerable<IdentityUser> GetIdentityUsersAsync();
    }
}
