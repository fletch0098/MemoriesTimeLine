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
        Task<IdentityUser> GetIdentityUserByIdAsync(string id);
        Task<IdentityUser> GetIdentityUserByUserNameAsync(string userName);
        Task<IdentityUser> GetIdentityUserByEmailAsync(string email);
        Task<IdentityResult> CreateIdentityUserAsync(IdentityUser identityUser, string password);
        Task UpdateIdentityUserAsync(string id, IdentityUser identityUser);
        Task DeleteIdentityUserAsync(string id);
        #endregion

        IEnumerable<IdentityUser> GetAllIdentityUsers();
    }
}
