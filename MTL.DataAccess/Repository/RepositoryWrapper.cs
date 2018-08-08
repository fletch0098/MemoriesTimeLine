using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Repository
{
    /// <summary>
    /// Repository for all Objects in Application
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private readonly ILogger<RepositoryWrapper> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        private ITimeLineRepository _timeLine;
        private IMemoryRepository _memory;
        private IAppUserRepository _appUser;
        private IUserProfileRepository _userProfile;
        private IIdentityUserRepository _identityUser;

        public RepositoryWrapper(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger, UserManager<IdentityUser> userManager)
        {
            _repoContext = repositoryContext;
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        /// TimeLine Objects
        /// </summary>
        public ITimeLineRepository TimeLines
        {
            get
            {
                if (_timeLine == null)
                {
                    _timeLine = new TimeLineRepository(_repoContext, _logger, _userManager);
                }

                return _timeLine;
            }
        }

        /// <summary>
        /// AppUsers
        /// </summary>
        public IAppUserRepository AppUsers
        {
            get
            {
                if (_appUser == null)
                {
                    _appUser = new AppUserRepository(_repoContext, _logger, _userManager);
                }

                return _appUser;
            }
        }

        /// <summary>
        /// Memory
        /// </summary>
        public IMemoryRepository Memories
        {
            get
            {
                if (_memory == null)
                {
                    _memory = new MemoryRepository(_repoContext, _logger);
                }

                return _memory;
            }
        }

        /// <summary>
        /// UserProfile
        /// </summary>
        public IUserProfileRepository UserProfiles
        {
            get
            {
                if (_userProfile == null)
                {
                    _userProfile = new UserProfileRepository(_repoContext, _logger, _userManager);
                }

                return _userProfile;
            }
        }

        /// <summary>
        /// ASP Identity Users
        /// </summary>
        public IIdentityUserRepository IdentityUsers
        {
            get
            {
                if (_identityUser == null)
                {
                    _identityUser = new IdentityUserRepository(_repoContext, _userManager, _logger);
                }

                return _identityUser;
            }
        }

    }
}
