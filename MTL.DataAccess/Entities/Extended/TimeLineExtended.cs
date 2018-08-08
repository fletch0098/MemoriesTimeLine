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

        public AppUser AppUser {get;set;}
        public IEnumerable<Memory> Memories { get; set; }

        public TimeLineExtended(TimeLine timeLine)
        {
            this.Id = timeLine.Id;
            this.Name = timeLine.Name;
            this.Description = timeLine.Description;
            this.LastModified = timeLine.LastModified;
        }
    }
}
