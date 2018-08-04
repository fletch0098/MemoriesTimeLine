using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;

namespace MTL.DataAccess.Repository
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
