using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MTL.DataAccess.Repository
{
    public class TimeLineRepository : RepositoryBase<TimeLine>, ITimeLineRepository
    {
        public TimeLineRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
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

        public TimeLineExtended GetTimeLineWithDetails(int id)
        {
            return new TimeLineExtended(GetTimeLineById(id))
            {
                Memories = RepositoryContext.Memories
                    .Where(x => x.TimeLineId == id)
            };
        }
    }
}
