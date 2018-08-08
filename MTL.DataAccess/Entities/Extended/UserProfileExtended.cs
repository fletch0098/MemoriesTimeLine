using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    [Table("UserProfiles")]
    public class UserProfileExtended : BaseEntity
    {
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }

        public AppUser AppUser {get;set;}

        public UserProfileExtended(UserProfile userProfile)
        {
            this.Id = userProfile.Id;
            this.Gender = userProfile.Gender;
            this.Locale = userProfile.Locale;
            this.Location = userProfile.Location;
            this.LastModified = userProfile.LastModified;
            this.FacebookId = userProfile.FacebookId;
            this.PictureUrl = this.PictureUrl;
        }
    }
}
