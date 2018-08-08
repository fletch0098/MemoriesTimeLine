using System.Collections.Generic;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;
using MTL.DataAccess.Entities.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Repository
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        //private readonly ILogger<RepositoryWrapper> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public AppUserRepository(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger, UserManager<IdentityUser> userManager)
        : base(repositoryContext, logger)
        {
            this._userManager = userManager;
        }


        #region SYNC
        public IEnumerable<AppUser> GetAllAppUsers()
        {
            return FindAll()
                .OrderBy(tl => tl.LastName);
        }

        public AppUser GetAppUserById(int id)
        {
            return FindByCondition(tl => tl.Id.Equals(id))
                .DefaultIfEmpty(new AppUser())
                .FirstOrDefault();
        }

        public IEnumerable<AppUser> GetAppUsersByIdentityId(string identityId)
        {
            return FindByCondition(tl => tl.IdentityId.Equals(identityId))
                .OrderBy(x => x.LastModified);
        }

        public AppUserExtended GetAppUserWithDetails(int id)
        {
            var appUser = GetAppUserById(id);

            var identity = _userManager.FindByIdAsync(appUser.IdentityId);

            return new AppUserExtended(appUser)
            {
                IdentityUser = identity.Result,
                UserProfile = RepositoryContext.UserProfiles
                    .Where(a => a.AppUserId == id).FirstOrDefault(),
            };
        }

        public int CreateAppUser(AppUser appUser)
        {
            Create(appUser);
            Save();
            return appUser.Id;
        }

        public void UpdateAppUser(int id,AppUser appUser)
        {
            AppUser dbAppUser = GetAppUserById(id);
            appUser.Modified();
            dbAppUser.Map(appUser);
            Update(dbAppUser);
            Save();
        }

        public void DeleteAppUser(int id)
        {
            AppUser dbAppUser = GetAppUserById(id);
            Delete(dbAppUser);
            Save();
        }

        #endregion

        #region ASYNC
        public async Task<IEnumerable<AppUser>> GetAllAppUsersAsync()
        {
            var appUsers = await FindAllAsync();
            return appUsers.OrderBy(x => x.LastModified);
        }

        public async Task<IEnumerable<AppUser>> GetAppUsersByIdentityIdAsync(string identityId)
        {
            var appUsers = await FindByConditionAync(o => o.IdentityId.Equals(identityId));
            return appUsers.OrderBy(x => x.LastModified);
        }

        public async Task<AppUser> GetAppUserByIdAsync(int id)
        {
            var appUser = await FindByConditionAync(o => o.Id.Equals(id));
            return appUser.DefaultIfEmpty(new AppUser())
                    .FirstOrDefault();
        }

        public async Task<AppUserExtended> GetAppUserWithDetailsAsync(int id)
        {
            var appUser = await GetAppUserByIdAsync(id);
            var IdentityUser = await _userManager.FindByIdAsync(appUser.IdentityId);
            var UserProfile = await RepositoryContext.UserProfiles
                    .Where(a => a.AppUserId == id).FirstOrDefaultAsync();

            return new AppUserExtended(appUser)
            {
                 IdentityUser = IdentityUser,
                UserProfile = UserProfile
            };
        }

        //public async Task<AppUserExtended> GetAppUserWithOwnerAsync(int id)
        //{
        //    var appUser = await GetAppUserByIdAsync(id);

        //    return new AppUserExtended(appUser)
        //    {
        //        Identity = await _userManager.FindByIdAsync(appUser.IdentityId)
        //    };
        //}

        public async Task<int> CreateAppUserAsync(AppUser appUser)
        {
            Create(appUser);
            await SaveAsync();
            return appUser.Id;
        }

        public async Task UpdateAppUserAsync(int id, AppUser appUser)
        {
            AppUser dbAppUser = await GetAppUserByIdAsync(id);
            dbAppUser.Modified();
            dbAppUser.Map(appUser);
            Update(dbAppUser);
            await SaveAsync();
        }

        public async Task DeleteAppUserAsync(int id)
        {
            AppUser dbAppUser = await GetAppUserByIdAsync(id);
            Delete(dbAppUser);
            await SaveAsync();
        }
        #endregion
    }
}
