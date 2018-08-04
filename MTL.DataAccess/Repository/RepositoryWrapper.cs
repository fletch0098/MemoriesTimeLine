using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;

namespace MTL.DataAccess.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ITimeLineRepository _timeLine;
        private IMemoryRepository _memory;
        private IUserProfileRepository _userProfile;

        public ITimeLineRepository TimeLine
        {
            get
            {
                if (_timeLine == null)
                {
                    _timeLine = new TimeLineRepository(_repoContext);
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
                    _memory = new MemoryRepository(_repoContext);
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
                    _userProfile = new UserProfileRepository(_repoContext);
                }

                return _userProfile;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
    }
}
