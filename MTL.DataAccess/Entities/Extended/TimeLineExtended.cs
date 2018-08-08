using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    [Table("TimeLines")]
    public class TimeLineExtended : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public String IdentityId { get; set; }

        public AppUser Identity {get;set;}
        public IEnumerable<Memory> Memories { get; set; }

        public TimeLineExtended(TimeLine timeLine)
        {
            this.Id = timeLine.Id;
            this.Name = timeLine.Name;
            this.Description = timeLine.Description;
            this.IdentityId = timeLine.IdentityId;
            this.LastModified = timeLine.LastModified;
        }
    }
}
