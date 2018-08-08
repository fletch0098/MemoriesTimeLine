using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    [Table("UserProfiles")]
    public class UserProfile : BaseEntity
    {
        public string IdentityId { get; set; }
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
    }
}
