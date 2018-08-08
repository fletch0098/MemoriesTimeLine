using System.Collections.Generic;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;
using MTL.DataAccess.Entities.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Repository
{
    /// <summary>
    /// Repository for TimeLine Objects
    /// </summary>
    public class TimeLineRepository : RepositoryBase<TimeLine>, ITimeLineRepository
    {
        //private readonly ILogger<RepositoryWrapper> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public TimeLineRepository(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger, UserManager<IdentityUser> userManager)
        : base(repositoryContext, logger)
        {
            this._userManager = userManager;
        }

        #region SYNC

        /// <summary>
        /// Synchronously Get a list of all TimeLines
        /// </summary>
        /// <returns>TimeLine[]</returns>
        public IEnumerable<TimeLine> GetAllTimeLines()
        {
            return FindAll()
                .OrderBy(tl => tl.Name);
        }

        /// <summary>
        /// Synchronously Get a single TimeLine by it´s Id
        /// </summary>
        /// <param name="id">Id of TimeLine</param>
        /// <returns>A single TimeLine</returns>
        public TimeLine GetTimeLineById(int id)
        {
            return FindByCondition(tl => tl.Id.Equals(id))
                .DefaultIfEmpty(new TimeLine())
                .FirstOrDefault();
        }

        /// <summary>
        /// Synchronously Get a list of TimeLines by AppUserId
        /// </summary>
        /// <param name="appUserId">Id of AppUser</param>
        /// <returns>TimeLine[]</returns>
        public IEnumerable<TimeLine> GetTimeLinesByAppUserId(int appUserId)
        {
            return FindByCondition(tl => tl.AppUserId.Equals(appUserId))
                .OrderBy(x => x.LastModified);
        }

        /// <summary>
        /// Synchronously Get a single ExtendedTimeLine by it´s Id
        /// Memory[]
        /// AppUser
        /// </summary>
        /// <param name="id">Id of TimeLine</param>
        /// <returns>A single TimeLine</returns>
        public TimeLineExtended GetTimeLineWithDetails(int id)
        {
            var timeLine = GetTimeLineById(id);

            return new TimeLineExtended(timeLine)
            {
                //Get all Memories for TimeLine
                Memories = RepositoryContext.Memories
                    .Where(x => x.TimeLineId == id).ToList(),

                //A single AppUser
                AppUser = RepositoryContext.AppUsers
                    .Where(a => a.Id == timeLine.AppUserId).FirstOrDefault()
            };
        }

        /// <summary>
        /// Synchronously Create a TimeLine
        /// </summary>
        /// <param name="timeLine">TimeLine to create</param>
        /// <returns>Id of the newly created TimeLine</returns>
        public int CreateTimeLine(TimeLine timeLine)
        {
            Create(timeLine);
            Save();
            return timeLine.Id;
        }

        /// <summary>
        /// Synchronously Update a TimeLine
        /// </summary>
        /// <param name="timeLine">TimeLine to create</param>
        /// <param name="id">Id of TimeLine to update</param>
        public void UpdateTimeLine(int id,TimeLine timeLine)
        {
            TimeLine dbTimeLine = GetTimeLineById(id);
            timeLine.Modified();
            dbTimeLine.Map(timeLine);
            Update(dbTimeLine);
            Save();
        }

        /// <summary>
        /// Synchronously Delete a TimeLine
        /// </summary>
        /// <param name="id">Id of TimeLine to delete</param>
        public void DeleteTimeLine(int id)
        {
            TimeLine dbTimeLine = GetTimeLineById(id);
            Delete(dbTimeLine);
            Save();
        }

        #endregion

        #region ASYNC

        /// <summary>
        /// Asynchronously Get a list of all TimeLines
        /// </summary>
        /// <returns>TimeLine[]</returns>
        public async Task<IEnumerable<TimeLine>> GetAllTimeLinesAsync()
        {
            var timeLines = await FindAllAsync();
            return timeLines.OrderBy(x => x.Name);
        }

        /// <summary>
        /// Asynchronously Get a single TimeLine by it´s Id
        /// </summary>
        /// <param name="id">Id of TimeLine</param>
        /// <returns>A single TimeLine</returns>
        public async Task<IEnumerable<TimeLine>> GetTimeLinesByAppUserIdAsync(int appUserId)
        {
            var timeLines = await FindByConditionAync(o => o.AppUserId.Equals(appUserId));
            return timeLines.OrderBy(x => x.LastModified);
        }

        /// <summary>
        /// Asynchronously Get a list of TimeLines by AppUserId
        /// </summary>
        /// <param name="appUserId">Id of AppUser</param>
        /// <returns>TimeLine[]</returns>
        public async Task<TimeLine> GetTimeLineByIdAsync(int id)
        {
            var timeLine = await FindByConditionAync(o => o.Id.Equals(id));
            return timeLine.DefaultIfEmpty(new TimeLine())
                    .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously Get a single ExtendedTimeLine by it´s Id
        /// Memory[]
        /// AppUser
        /// </summary>
        /// <param name="id">Id of TimeLine</param>
        /// <returns>A single TimeLine</returns>
        public async Task<TimeLineExtended> GetTimeLineWithDetailsAsync(int id)
        {
            var timeLine = await GetTimeLineByIdAsync(id);

            return new TimeLineExtended(timeLine)
            {
                Memories = await RepositoryContext.Memories
                    .Where(a => a.TimeLineId == id).ToListAsync(),
                AppUser = await RepositoryContext.AppUsers
                    .Where(a => a.Id == timeLine.AppUserId).FirstOrDefaultAsync()
            };
        }

        /// <summary>
        /// Asynchronously Create a TimeLine
        /// </summary>
        /// <param name="timeLine">TimeLine to create</param>
        /// <returns>Id of the newly created TimeLine</returns>
        public async Task<int> CreateTimeLineAsync(TimeLine timeLine)
        {
            Create(timeLine);
            await SaveAsync();
            return timeLine.Id;
        }

        /// <summary>
        /// Asynchronously Update a TimeLine
        /// </summary>
        /// <param name="timeLine">TimeLine to create</param>
        /// <param name="id">Id of TimeLine to update</param>
        public async Task UpdateTimeLineAsync(int id, TimeLine timeLine)
        {
            TimeLine dbTimeLine = await GetTimeLineByIdAsync(id);
            dbTimeLine.Modified();
            dbTimeLine.Map(timeLine);
            Update(dbTimeLine);
            await SaveAsync();
        }

        /// <summary>
        /// Asynchronously Delete a TimeLine
        /// </summary>
        /// <param name="id">Id of TimeLine to delete</param>
        public async Task DeleteTimeLineAsync(int id)
        {
            TimeLine dbTimeLine = await GetTimeLineByIdAsync(id);
            Delete(dbTimeLine);
            await SaveAsync();
        }
        #endregion
    }
}
