using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.DataAccess.Entities.Extensions
{
    /// <summary>
    /// TimeLine entity extensions
    /// </summary>
    public static class TimeLineExtensions
    {
        /// <summary>
        /// Map the TimeLine to the dbTimeLine
        /// </summary>
        /// <param name="dbTimeLine">The Timeline in the database</param>
        /// <param name="timeLine">The modified TimeLine</param>
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
