using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    /// <summary>
    /// Extended TimeLine with AppUser and Memory[]
    /// </summary>
    [Table("TimeLines")]
    public class TimeLineExtended : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// AppUser 
        /// </summary>
        public AppUser AppUser {get;set;}

        /// <summary>
        /// A list of Memory objects
        /// </summary>
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
