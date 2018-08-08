using System;
using System.Collections.Generic;
using MTL.DataAccess.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Contracts
{
    public interface IAppUserRepository
    {
        #region ASYNC
        Task<IdentityResult> CreateAppUserAsync(AppUser entity, string password);
        Task<AppUser> FindByNameAsync(string userName);
        Task<AppUserExtended> FindExtendedByNameAsync(string userName);
        Task<AppUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(AppUser userToVerify, string password);
        #endregion
    }
}
