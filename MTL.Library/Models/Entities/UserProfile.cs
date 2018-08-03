using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.Library.Models.Entities
{
    public class UserProfile
    {
        public int id { get; set; }
        public string identityId { get; set; }
        public string location { get; set; }
        public string locale { get; set; }
        public string gender { get; set; }
        public DateTime lastModified { get; set; }

        public virtual AppUser identity { get; set; }  // navigation property




    }
}
