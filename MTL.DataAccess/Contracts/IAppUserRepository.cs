using System.Collections.Generic;
using MTL.DataAccess.Entities;
using System.Threading.Tasks;

namespace MTL.DataAccess.Contracts
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
        #region SYNC
        IEnumerable<AppUser> GetAllAppUsers();
        AppUser GetAppUserById(int id);
        IEnumerable<AppUser> GetAppUsersByIdentityId(string identityId);
        AppUserExtended GetAppUserWithDetails(int id);
        int CreateAppUser(AppUser appUser);
        void UpdateAppUser(int id, AppUser appUser);
        void DeleteAppUser(int id);
        #endregion

        #region ASYNC
        Task<IEnumerable<AppUser>> GetAllAppUsersAsync();
        Task<AppUser> GetAppUserByIdAsync(int id);
        Task<IEnumerable<AppUser>> GetAppUsersByIdentityIdAsync(string identityId);
        Task<AppUserExtended> GetAppUserWithDetailsAsync(int Id);
        Task<int> CreateAppUserAsync(AppUser appUser);
        Task UpdateAppUserAsync(int id, AppUser appUser);
        Task DeleteAppUserAsync(int id);
        #endregion
    }
}
