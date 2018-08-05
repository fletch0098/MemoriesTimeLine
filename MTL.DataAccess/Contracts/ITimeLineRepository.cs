using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Entities;
using MTL.DataAccess.Repository;

namespace MTL.DataAccess.Contracts
{
    public interface ITimeLineRepository : IRepositoryBase<TimeLine>
    {
        IEnumerable<TimeLine> GetAllTimeLines();
        TimeLine GetTimeLineById(int id);
        TimeLineExtended GetTimeLineWithDetails(int id);

    }
}
