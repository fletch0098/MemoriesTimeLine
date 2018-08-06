using System.Collections.Generic;
using MTL.DataAccess.Entities;
using System.Threading.Tasks;

namespace MTL.DataAccess.Contracts
{
    public interface ITimeLineRepository : IRepositoryBase<TimeLine>
    {
        #region SYNC
        IEnumerable<TimeLine> GetAllTimeLines();
        TimeLine GetTimeLineById(int id);
        TimeLine GetTimeLineByOwnerId(int ownerId);
        TimeLineExtended GetTimeLineWithDetails(int id);
        int CreateTimeLine(TimeLine timeLine);
        void UpdateTimeLine(int id, TimeLine timeLine);
        void DeleteTimeLine(int id);
        #endregion

        #region ASYNC
        Task<IEnumerable<TimeLine>> GetAllTimeLinesAsync();
        Task<TimeLine> GetTimeLineByIdAsync(int id);
        Task<TimeLineExtended> GetTimeLineWithDetailsAsync(int Id);
        Task<int> CreateTimeLineAsync(TimeLine timeLine);
        Task UpdateTimeLineAsync(int id, TimeLine timeLine);
        Task DeleteTimeLineAsync(int id);
        #endregion
    }
}
