using System;
using System.Collections.Generic;
using System.Text;
using MTL.Library.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Data;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace MTL.DataAccess.RepositoryManager
{
    public class TimeLineManager : IDataRepository<TimeLine, int>
    {
        MyAppContext ctx;
        private readonly ILogger<TimeLineManager> _logger;

        public TimeLineManager(MyAppContext c, ILogger<TimeLineManager> logger)
        {
            ctx = c;
            _logger = logger;
        }

        public TimeLine Get(int id)
        {
            TimeLine ret = null;

            try
            {
                var query = (from q in ctx.TimeLines.Include("memories")
                             where q.id == id
                             select q).FirstOrDefault();

                if (query != null)
                {
                    ret = query;
                    _logger.LogInformation(string.Format("{0} : Found TimeLine with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No TimeLines were found for id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while looking for ControlledEntrie id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
            }

            return ret;
        }

        public IEnumerable<TimeLine> GetAll()
        {
            IEnumerable<TimeLine> ret = null;

            try
            {
                var query = ctx.TimeLines.Include("memories");

                if (query != null)
                {
                    ret = query.ToList();
                    _logger.LogInformation(string.Format("{0} : {1} TimeLines found", System.Reflection.MethodBase.GetCurrentMethod(), ret.Count()));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No TimeLines were found", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while looking for TimeLines", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Add(TimeLine TimeLine)
        {
            int ret = 0;

            try
            {
                TimeLine.lastModified = DateTime.Now;
                ctx.TimeLines.Add(TimeLine);
                var updatedItems = ctx.SaveChanges();

                if (updatedItems > 0)
                {
                    ret = TimeLine.id;

                    _logger.LogInformation(string.Format("{0} : Added TimeLine with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No TimeLines were added", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while adding to TimeLines", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Delete(int id)
        {
            int ret = 0;

            try
            {
                var TimeLine = ctx.TimeLines.FirstOrDefault(b => b.id == id);
                if (TimeLine != null)
                {
                    ctx.TimeLines.Remove(TimeLine);
                    var updatedItems = ctx.SaveChanges();

                    if (updatedItems > 0)
                    {
                        ret = TimeLine.id;
                        _logger.LogInformation(string.Format("{0} : Deleted TimeLine with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                    }
                    else
                    {
                        _logger.LogWarning(string.Format("{0} : No TimeLines were deleted", System.Reflection.MethodBase.GetCurrentMethod()));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while deleting from TimeLines", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Update(int id, TimeLine item)
        {
            int ret = 0;

            try
            {
                var TimeLine = ctx.TimeLines.Find(id);
                if (TimeLine != null)
                {
                    TimeLine.description = item.description;
                    TimeLine.name = item.name;
                    TimeLine.lastModified = DateTime.Now;

                    ret = ctx.SaveChanges();

                    ret = TimeLine.id;
                    _logger.LogInformation(string.Format("{0} : Updated TimeLine with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No TimeLines were updated", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while updating from TimeLines", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }
    }
}
