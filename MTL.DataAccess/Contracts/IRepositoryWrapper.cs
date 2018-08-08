using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.DataAccess.Contracts
{
    public interface IRepositoryWrapper
    {
        ITimeLineRepository TimeLines { get; }
        IMemoryRepository Memories { get; }
        IUserProfileRepository UserProfiles { get; }
        IIdentityUserRepository IdentityUsers { get; }
        IAppUserRepository AppUsers { get; }
    }
}
