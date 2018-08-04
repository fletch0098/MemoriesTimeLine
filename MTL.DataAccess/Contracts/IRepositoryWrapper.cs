using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.DataAccess.Contracts
{
    public interface IRepositoryWrapper
    {
        ITimeLineRepository TimeLine { get; }
        IMemoryRepository Memory { get; }
        IUserProfileRepository UserProfile { get; }
    }
}
