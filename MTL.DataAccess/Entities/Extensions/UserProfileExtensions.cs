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
            dbUserProfile.IdentityId = userProfile.IdentityId;
            dbUserProfile.LastModified = userProfile.LastModified;
            dbUserProfile.Locale = userProfile.Locale;
            dbUserProfile.Location = userProfile.Location;
        }
    }
}
