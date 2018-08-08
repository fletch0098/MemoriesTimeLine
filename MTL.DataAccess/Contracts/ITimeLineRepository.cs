using System.Collections.Generic;
using MTL.DataAccess.Entities;
using System.Threading.Tasks;

namespace MTL.DataAccess.Contracts
{
    /// <summary>
    /// Repository for TimeLine Objects
    /// </summary>
    public interface ITimeLineRepository : IRepositoryBase<TimeLine>
    {
        #region SYNC
        IEnumerable<TimeLine> GetAllTimeLines();
        TimeLine GetTimeLineById(int id);
        IEnumerable<TimeLine> GetTimeLinesByAppUserId(int appUserId);
        TimeLineExtended GetTimeLineWithDetails(int id);
        //TimeLineExtended GetTimeLineWithOwner(int id);
        int CreateTimeLine(TimeLine timeLine);
        void UpdateTimeLine(int id, TimeLine timeLine);
        void DeleteTimeLine(int id);
        #endregion

        #region ASYNC
        /// <summary>
        /// Asynchronously Get a list of all TimeLines
        /// </summary>
        /// <returns>TimeLine[]</returns>
        Task<IEnumerable<TimeLine>> GetAllTimeLinesAsync();
        Task<TimeLine> GetTimeLineByIdAsync(int id);
        Task<IEnumerable<TimeLine>> GetTimeLinesByAppUserIdAsync(int appUserId);
        Task<TimeLineExtended> GetTimeLineWithDetailsAsync(int Id);
        //Task<TimeLineExtended> GetTimeLineWithOwnerAsync(int Id);
        Task<int> CreateTimeLineAsync(TimeLine timeLine);
        Task UpdateTimeLineAsync(int id, TimeLine timeLine);
        Task DeleteTimeLineAsync(int id);
        #endregion
    }
}
