using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    [Table("UserProfiles")]
    public class UserProfile : BaseEntity
    {
        [Required(ErrorMessage = "AppUser is required")]
        public int AppUserId { get; set; }

        [StringLength(60, ErrorMessage = "Location can't be longer than 60 characters")]
        public string Location { get; set; }

        [StringLength(60, ErrorMessage = "Locale can't be longer than 60 characters")]
        public string Locale { get; set; }

        [StringLength(60, ErrorMessage = "Gender can't be longer than 60 characters")]
        public string Gender { get; set; }

        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }

        public UserProfile()
        {
            this.Modified();
        }
    }
}
