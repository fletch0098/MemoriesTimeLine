using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.DataAccess.Entities.Extensions
{
    public static class TimeLineExtensions
    {
        public static void Map(this TimeLine dbTimeLine, TimeLine timeLine)
        {
            dbTimeLine.Name = timeLine.Name;
            dbTimeLine.Description = timeLine.Description;
            dbTimeLine.LastModified = timeLine.LastModified;
            dbTimeLine.AppUserId = timeLine.AppUserId;
            dbTimeLine.Id = timeLine.Id;
        }
    }
}
