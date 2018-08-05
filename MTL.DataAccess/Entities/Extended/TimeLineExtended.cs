﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Entities
{
    [Table("TimeLines")]
    public class TimeLineExtended : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public String OwnerId { get; set; }

        public AppUser Owner {get;set;}
        public IEnumerable<Memory> Memories { get; set; }

        public TimeLineExtended(TimeLine timeLine)
        {
            this.Id = timeLine.Id;
            this.Name = timeLine.Name;
            this.Description = timeLine.Description;
            this.OwnerId = timeLine.OwnerId;
            this.LastModified = timeLine.LastModified;
        }
    }
}