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
    public class UserProfileManager : IDataRepository<UserProfile, int>
    {
        MyAppContext ctx;
        private readonly ILogger<UserProfileManager> _logger;

        public UserProfileManager(MyAppContext c, ILogger<UserProfileManager> logger)
        {
            ctx = c;
            _logger = logger;
        }

        public UserProfile Get(int id)
        {
            UserProfile ret = null;

            try
            {
                var query = (from q in ctx.UserProfile
                             where q.id == id
                             select q).FirstOrDefault();

                if (query != null)
                {
                    ret = query;
                    _logger.LogInformation(string.Format("{0} : Found UserProfile with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No UserProfiles were found for id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while looking for ControlledEntrie id {1}", System.Reflection.MethodBase.GetCurrentMethod(), id));
            }

            return ret;
        }

        public IEnumerable<UserProfile> GetAll()
        {
            IEnumerable<UserProfile> ret = null;

            try
            {
                var query = ctx.UserProfile;

                if (query != null)
                {
                    ret = query.ToList();
                    _logger.LogInformation(string.Format("{0} : {1} UserProfiles found", System.Reflection.MethodBase.GetCurrentMethod(), ret.Count()));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No UserProfiles were found", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while looking for UserProfiles", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Add(UserProfile UserProfile)
        {
            int ret = 0;

            try
            {
                UserProfile.lastModified = DateTime.Now;
                ctx.UserProfile.Add(UserProfile);
                var updatedItems = ctx.SaveChanges();

                if (updatedItems > 0)
                {
                    ret = UserProfile.id;

                    _logger.LogInformation(string.Format("{0} : Added UserProfile with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No UserProfiles were added", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while adding to UserProfiles", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Delete(int id)
        {
            int ret = 0;

            try
            {
                var UserProfile = ctx.UserProfile.FirstOrDefault(b => b.id == id);
                if (UserProfile != null)
                {
                    ctx.UserProfile.Remove(UserProfile);
                    var updatedItems = ctx.SaveChanges();

                    if (updatedItems > 0)
                    {
                        ret = UserProfile.id;
                        _logger.LogInformation(string.Format("{0} : Deleted UserProfile with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                    }
                    else
                    {
                        _logger.LogWarning(string.Format("{0} : No UserProfiles were deleted", System.Reflection.MethodBase.GetCurrentMethod()));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while deleting from UserProfiles", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }

        public int Update(int id, UserProfile item)
        {
            int ret = 0;

            try
            {
                var UserProfile = ctx.UserProfile.Find(id);
                if (UserProfile != null)
                {
                    UserProfile.gender = item.gender;
                    UserProfile.identity = item.identity;
                    UserProfile.identityId = item.identityId;
                    UserProfile.locale = item.locale;
                    UserProfile.location = item.location;
                    UserProfile.lastModified = DateTime.Now;

                    ret = ctx.SaveChanges();

                    ret = UserProfile.id;
                    _logger.LogInformation(string.Format("{0} : Updated UserProfile with Id {1}", System.Reflection.MethodBase.GetCurrentMethod(), ret));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No UserProfiles were updated", System.Reflection.MethodBase.GetCurrentMethod()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while updating from UserProfiles", System.Reflection.MethodBase.GetCurrentMethod()));
            }

            return ret;
        }
    }
}
