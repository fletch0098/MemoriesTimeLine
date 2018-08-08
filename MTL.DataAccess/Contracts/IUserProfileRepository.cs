using System.Collections.Generic;
using MTL.DataAccess.Entities;
using System.Threading.Tasks;

namespace MTL.DataAccess.Contracts
{
    public interface IUserProfileRepository : IRepositoryBase<UserProfile>
    {
        #region SYNC
        IEnumerable<UserProfile> GetAllUserProfiles();
        UserProfile GetUserProfileById(int id);
        UserProfile GetUserProfileByAppUserId(int appUserId);
        UserProfileExtended GetUserProfileWithDetails(int id);
        int CreateUserProfile(UserProfile userProfile);
        void UpdateUserProfile(int id, UserProfile userProfile);
        void DeleteUserProfile(int id);
        #endregion

        #region ASYNC
        Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync();
        Task<UserProfile> GetUserProfileByIdAsync(int id);
        Task<UserProfileExtended> GetUserProfileWithDetailsAsync(int Id);
        Task<UserProfile> GetUserProfileByAppUserIdAsync(int appUserId);
        Task<int> CreateUserProfileAsync(UserProfile userProfile);
        Task UpdateUserProfileAsync(int id, UserProfile userProfile);
        Task DeleteUserProfileAsync(int id);
        #endregion
    }
}
