using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private readonly ILogger<RepositoryWrapper> _logger;
        private readonly UserManager<AppUser> _userManager;

        private ITimeLineRepository _timeLine;
        private IMemoryRepository _memory;
        private IUserProfileRepository _userProfile;
        private IAppUserRepository _appUser;

        public RepositoryWrapper(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger, UserManager<AppUser> userManager)
        {
            _repoContext = repositoryContext;
            _logger = logger;
            _userManager = userManager;
        }

        public ITimeLineRepository TimeLine
        {
            get
            {
                if (_timeLine == null)
                {
                    _timeLine = new TimeLineRepository(_repoContext, _logger);
                }

                return _timeLine;
            }
        }

        public IMemoryRepository Memory
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

        public IUserProfileRepository UserProfile
        {
            get
            {
                if (_userProfile == null)
                {
                    _userProfile = new UserProfileRepository(_repoContext, _logger);
                }

                return _userProfile;
            }
        }

        public IAppUserRepository AppUser
        {
            get
            {
                if (_appUser == null)
                {
                    _appUser = new AppUserRepository(_repoContext, _userManager, _logger);
                }

                return _appUser;
            }
        }

    }
}
