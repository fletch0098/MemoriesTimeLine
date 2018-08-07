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
    public class TimeLineRepository : RepositoryBase<TimeLine>, ITimeLineRepository
    {
        private readonly ILogger<RepositoryWrapper> _logger;
        private readonly UserManager<AppUser> _userManager;

        #region SYNC
        public TimeLineRepository(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger, UserManager<AppUser> userManager)
            : base(repositoryContext, logger)
        {
            this._userManager = userManager;
        }

        public IEnumerable<TimeLine> GetAllTimeLines()
        {
            return FindAll()
                .OrderBy(tl => tl.Name);
        }

        public TimeLine GetTimeLineById(int id)
        {
            return FindByCondition(tl => tl.Id.Equals(id))
                .DefaultIfEmpty(new TimeLine())
                .FirstOrDefault();
        }

        public IEnumerable<TimeLine> GetTimeLinesByOwnerId(int ownerId)
        {
            return FindByCondition(tl => tl.OwnerId.Equals(ownerId))
                .OrderBy(x => x.LastModified);
        }

        public TimeLineExtended GetTimeLineWithDetails(int id)
        {
            return new TimeLineExtended(GetTimeLineById(id))
            {
                Memories = RepositoryContext.Memories
                    .Where(x => x.TimeLineId == id)
            };
        }

        public TimeLineExtended GetTimeLineWithOwner(int id)
        {
            var timeLine =  GetTimeLineById(id);

            return new TimeLineExtended(timeLine)
            {
                Owner = _userManager.Users
                .Where(x => x.Id == timeLine.OwnerId)
                .FirstOrDefault()
            };
        }

        public int CreateTimeLine(TimeLine timeLine)
        {
            Create(timeLine);
            Save();
            return timeLine.Id;
        }

        public void UpdateTimeLine(int id,TimeLine timeLine)
        {
            TimeLine dbTimeLine = GetTimeLineById(id);
            timeLine.Modified();
            dbTimeLine.Map(timeLine);
            Update(dbTimeLine);
            Save();
        }

        public void DeleteTimeLine(int id)
        {
            TimeLine dbTimeLine = GetTimeLineById(id);
            Delete(dbTimeLine);
            Save();
        }

        #endregion

        #region ASYNC
        public async Task<IEnumerable<TimeLine>> GetAllTimeLinesAsync()
        {
            var timeLines = await FindAllAsync();
            return timeLines.OrderBy(x => x.Name);
        }

        public async Task<IEnumerable<TimeLine>> GetTimeLinesByOwnerIdAsync(string ownerId)
        {
            var timeLines = await FindByConditionAync(o => o.OwnerId.Equals(ownerId));
            return timeLines.OrderBy(x => x.LastModified);
        }

        public async Task<TimeLine> GetTimeLineByIdAsync(int id)
        {
            var timeLine = await FindByConditionAync(o => o.Id.Equals(id));
            return timeLine.DefaultIfEmpty(new TimeLine())
                    .FirstOrDefault();
        }

        public async Task<TimeLineExtended> GetTimeLineWithDetailsAsync(int id)
        {
            var timeLine = await GetTimeLineByIdAsync(id);

            return new TimeLineExtended(timeLine)
            {
                Memories = await RepositoryContext.Memories
                    .Where(a => a.TimeLineId == id).ToListAsync()
            };
        }

        public async Task<TimeLineExtended> GetTimeLineWithOwnerAsync(int id)
        {
            var timeLine = await GetTimeLineByIdAsync(id);

            return new TimeLineExtended(timeLine)
            {
                Owner = await _userManager.FindByIdAsync(timeLine.OwnerId)
            };
        }

        public async Task<int> CreateTimeLineAsync(TimeLine timeLine)
        {
            Create(timeLine);
            await SaveAsync();
            return timeLine.Id;
        }

        public async Task UpdateTimeLineAsync(int id, TimeLine timeLine)
        {
            TimeLine dbTimeLine = await GetTimeLineByIdAsync(id);
            dbTimeLine.Modified();
            dbTimeLine.Map(timeLine);
            Update(dbTimeLine);
            await SaveAsync();
        }

        public async Task DeleteTimeLineAsync(int id)
        {
            TimeLine dbTimeLine = await GetTimeLineByIdAsync(id);
            Delete(dbTimeLine);
            await SaveAsync();
        }
        #endregion
    }
}
