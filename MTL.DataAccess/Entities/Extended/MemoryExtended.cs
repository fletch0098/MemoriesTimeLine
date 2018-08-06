using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Entities
{
    [Table("Memories")]
    public class MemoryExtended : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int TimeLineId { get; set; }

        public TimeLine TimeLine { get; set; }

        public MemoryExtended(Memory memory)
        {
            this.Id = memory.Id;
            this.Name = memory.Name;
            this.Description = memory.Description;
            this.Date = memory.Date;
            this.LastModified = memory.LastModified;
        }
    }
}
