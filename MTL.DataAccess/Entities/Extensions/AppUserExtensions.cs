using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.DataAccess.Entities.Extensions
{
    public static class AppUserExtensions
    {
        public static void Map(this AppUser dbAppUser, AppUser appUser)
        {
            dbAppUser.LastModified = appUser.LastModified;
            dbAppUser.FirstName = appUser.FirstName;
            dbAppUser.IdentityId = dbAppUser.IdentityId;
            dbAppUser.LastName = dbAppUser.LastName;
            dbAppUser.Id = appUser.Id;
        }
    }
}
