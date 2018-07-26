using System;
using System.Collections.Generic;
using System.Text;
using MTL.Library.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MTL.DataAccess.RepositoryManager
{
    public class MemoryManager : IDataRepository<Memory, int>
    {
        MyAppContext ctx;
        private readonly ILogger<MemoryManager> _logger;

        public MemoryManager(MyAppContext c, ILogger<MemoryManager> logger)
        {
            ctx = c;
            _logger = logger;
        }

        public Memory Get(int id)
        {
            Memory ret = null;

            try
            {
                var query = (from q in ctx.Memories
                             where q.id == id
                             select q).FirstOrDefault();

                if (query != null)
                {
                    ret = query;
                    _logger.LogInformation(string.Format("{0} : Found Memory with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No Memories were found for id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while looking for ControlledEntrie id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
            }

            return ret;
        }

        public IEnumerable<Memory> GetAll()
        {
            IEnumerable<Memory> ret = null;

            try
            {
                var query = ctx.Memories;

                if (query != null)
                {
                    ret = query.ToList();
                    _logger.LogInformation(string.Format("{0} : {1} Memories found", System.Reflection.MethodBase.GetCurrentMethod(), ret.Count()));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No Memories were found", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while looking for Memories", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Add(Memory Memory)
        {
            int ret = 0;

            try
            {
                Memory.lastModified = DateTime.Now;
                ctx.Memories.Add(Memory);
                var updatedItems = ctx.SaveChanges();

                if (updatedItems > 0)
                {
                    ret = Memory.id;

                    _logger.LogInformation(string.Format("{0} : Added Memory with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No Memories were added", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while adding to Memories", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Delete(int id)
        {
            int ret = 0;

            try
            {
                var Memory = ctx.Memories.FirstOrDefault(b => b.id == id);
                if (Memory != null)
                {
                    ctx.Memories.Remove(Memory);
                    var updatedItems = ctx.SaveChanges();

                    if (updatedItems > 0)
                    {
                        ret = Memory.id;
                        _logger.LogInformation(string.Format("{0} : Deleted Memory with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                    }
                    else
                    {
                        _logger.LogWarning(string.Format("{0} : No Memories were deleted", System.Reflection.MethodBase.GetCurrentMethod()));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while deleting from Memories", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Update(int id, Memory item)
        {
            int ret = 0;

            try
            {
                var Memory = ctx.Memories.Find(id);
                if (Memory != null)
                {
                    Memory.description = item.description;
                    Memory.name = item.name;
                    Memory.lastModified = DateTime.Now;

                    ret = ctx.SaveChanges();

                    ret = Memory.id;
                    _logger.LogInformation(string.Format("{0} : Updated Memory with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No Memories were updated", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while updating from Memories", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }
    }
}
