using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;

namespace MTL.DataAccess.Repository
{
    public class TimeLineRepository : RepositoryBase<TimeLine>, ITimeLineRepository
    {
        public TimeLineRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
