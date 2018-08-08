using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.DataAccess.Entities.Extensions
{
    public static class UserProfileExtensions
    {
        public static void Map(this UserProfile dbUserProfile, UserProfile userProfile)
        {
            dbUserProfile.Gender = userProfile.Gender;
            dbUserProfile.Id = userProfile.Id;
            dbUserProfile.AppUserId = userProfile.AppUserId;
            dbUserProfile.LastModified = userProfile.LastModified;
            dbUserProfile.Locale = userProfile.Locale;
            dbUserProfile.Location = userProfile.Location;
            dbUserProfile.FacebookId = userProfile.FacebookId;
            dbUserProfile.PictureUrl = userProfile.PictureUrl;
        }
    }
}
