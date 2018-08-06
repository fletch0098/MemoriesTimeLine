using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    [Table("UserProfiles")]
    public class UserProfileExtended : BaseEntity
    {
        public string IdentityId { get; set; }
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public AppUser Owner {get;set;}

        public UserProfileExtended(UserProfile userProfile)
        {
            this.Id = userProfile.Id;
            this.Gender = userProfile.Gender;
            this.IdentityId = userProfile.IdentityId;
            this.Locale = userProfile.Locale;
            this.Location = userProfile.Location;
            this.LastModified = userProfile.LastModified;
        }
    }
}
