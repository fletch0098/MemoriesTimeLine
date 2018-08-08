using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Entities
{
    public class AppUserExtended : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserProfile UserProfile { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public AppUserExtended(AppUser appUser)
        {
            this.FirstName = appUser.FirstName;
            this.LastName = appUser.LastName;
            this.Id = appUser.Id;
            this.LastModified = appUser.LastModified;
        }
    }
}
