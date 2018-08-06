using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using Microsoft.AspNetCore.Identity;
using MTL.DataAccess.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MTL.DataAccess.Entities.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

// var localUser = await _userManager.FindByNameAsync(userInfo.Email);

namespace MTL.DataAccess.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        protected RepositoryContext RepositoryContext { get; set; }
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RepositoryWrapper> _logger;

        public AppUserRepository(RepositoryContext repositoryContext, UserManager<AppUser> userManager, ILogger<RepositoryWrapper> logger)
        {
            this.RepositoryContext = repositoryContext;
            this._userManager = userManager;
            this._logger = logger;
        }

        #region ASYNC
        public async Task<IdentityResult> CreateAppUserAsync(AppUser entity, string password)
        {
            var result = await _userManager.CreateAsync(entity, password);
            return result;
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<bool> CheckPasswordAsync(AppUser userToVerify, string password)
        {
            var result = await _userManager.CheckPasswordAsync(userToVerify, password);
            return result;
        }
        #endregion
    }
}
