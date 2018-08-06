using System.Collections.Generic;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;
using MTL.DataAccess.Entities.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace MTL.DataAccess.Repository
{
    public class TimeLineRepository : RepositoryBase<TimeLine>, ITimeLineRepository
    {
        private readonly ILogger<RepositoryWrapper> _logger;

        #region SYNC
        public TimeLineRepository(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger)
            : base(repositoryContext, logger)
        {
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

        public TimeLine GetTimeLineByOwnerId(int ownerId)
        {
            return FindByCondition(tl => tl.OwnerId.Equals(ownerId))
                .DefaultIfEmpty(new TimeLine())
                .FirstOrDefault();
        }

        public TimeLineExtended GetTimeLineWithDetails(int id)
        {
            return new TimeLineExtended(GetTimeLineById(id))
            {
                Memories = RepositoryContext.Memories
                    .Where(x => x.TimeLineId == id)
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
