﻿using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;
using MTL.DataAccess.Entities.Extensions;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Repository
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        private readonly ILogger<RepositoryWrapper> _logger;
        private readonly UserManager<AppUser> _userManager;

        public UserProfileRepository(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger, UserManager<AppUser> userManager)
            : base(repositoryContext, logger)
        {
            this._logger = logger;
            this._userManager = userManager;
        }

        #region SYNC
        public IEnumerable<UserProfile> GetAllUserProfiles()
        {
            return FindAll()
                .OrderBy(x => x.LastModified);
        }

        public UserProfile GetUserProfileById(int id)
        {
            return FindByCondition(tl => tl.Id.Equals(id))
                .DefaultIfEmpty(new UserProfile())
                .FirstOrDefault();
        }

        public UserProfile GetUserProfileByIdentityId(string identityId)
        {
            return FindByCondition(x => x.IdentityId.Equals(identityId))
                .DefaultIfEmpty(new UserProfile())
                .FirstOrDefault();
        }

        public UserProfileExtended GetUserProfileWithDetails(int id)
        {
            var userProfile = GetUserProfileById(id);
            return new UserProfileExtended(userProfile)
            {
                Owner = _userManager.Users
                .Where(x => x.Id == userProfile.IdentityId)
                .FirstOrDefault()
            };
        }

        public int CreateUserProfile(UserProfile userProfile)
        {
            Create(userProfile);
            Save();
            return userProfile.Id;
        }

        public void UpdateUserProfile(int id, UserProfile userProfile)
        {
            UserProfile dbUserProfile = GetUserProfileById(id);
            userProfile.Modified();
            dbUserProfile.Map(userProfile);
            Update(dbUserProfile);
            Save();
        }

        public void DeleteUserProfile(int id)
        {
            UserProfile dbUserProfile = GetUserProfileById(id);
            Delete(dbUserProfile);
            Save();
        }
        #endregion

        #region ASYNC
        public async Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync()
        {
            var userProfiles = await FindAllAsync();
            return userProfiles.OrderBy(x => x.Id);
        }

        public async Task<UserProfile> GetUserProfileByIdAsync(int id)
        {
            var userProfile = await FindByConditionAync(o => o.Id.Equals(id));
            return userProfile.DefaultIfEmpty(new UserProfile())
                    .FirstOrDefault();
        }

        public async Task<UserProfileExtended> GetUserProfileWithDetailsAsync(int id)
        {
            var userProfile = await GetUserProfileByIdAsync(id);

            return new UserProfileExtended(userProfile)
            {
                Owner = await _userManager.FindByIdAsync(userProfile.IdentityId)
            };
        }

        public async Task<UserProfile> GetUserProfileByIdentityIdAsync(string identityId)
        {
            var userProfile = await FindByConditionAync(x => x.IdentityId.Equals(identityId));
            return userProfile.FirstOrDefault();
        }

        public async Task<int> CreateUserProfileAsync(UserProfile userProfile)
        {
            Create(userProfile);
            await SaveAsync();
            return userProfile.Id;
        }

        public async Task UpdateUserProfileAsync(int id, UserProfile userProfile)
        {
            UserProfile dbUserProfile = await GetUserProfileByIdAsync(id);
            dbUserProfile.Modified();
            dbUserProfile.Map(userProfile);
            Update(dbUserProfile);
            await SaveAsync();
        }

        public async Task DeleteUserProfileAsync(int id)
        {
            UserProfile dbUserProfile = await GetUserProfileByIdAsync(id);
            Delete(dbUserProfile);
            await SaveAsync();
        }
        #endregion
    }
}
