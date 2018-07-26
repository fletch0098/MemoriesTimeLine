using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.Library.Models.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
    }
}
