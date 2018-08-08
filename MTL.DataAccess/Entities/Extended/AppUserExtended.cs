using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Entities
{
    public class AppUserExtended : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserProfile UserProfile {get;set;}

        public AppUserExtended(AppUser appUser)
        {
            this.Id = appUser.Id;
            this.AccessFailedCount = appUser.AccessFailedCount;
            this.ConcurrencyStamp = appUser.ConcurrencyStamp;
            this.EmailConfirmed = appUser.EmailConfirmed;
            this.FirstName = appUser.FirstName;
            this.LastName = appUser.LastName;
            this.LockoutEnabled = appUser.LockoutEnabled;
            this.LockoutEnd = appUser.LockoutEnd;
            this.NormalizedEmail = appUser.NormalizedEmail;
            this.NormalizedUserName = appUser.NormalizedUserName;
            this.PasswordHash = appUser.PasswordHash;
            this.PhoneNumber = appUser.PhoneNumber;
            this.PhoneNumberConfirmed = appUser.PhoneNumberConfirmed;
            this.SecurityStamp = appUser.SecurityStamp;
            this.TwoFactorEnabled = appUser.TwoFactorEnabled;
            this.UserName = appUser.UserName;

        }
    }
}
