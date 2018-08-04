using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.Library.Models.Entities
{
    [Table("timeLines")]
    public class TimeLine
    {
        //Properties
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        //[StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        //[StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string description { get; set; }

        public String ownerId { get; set; }
        public DateTime lastModified { get; set; }

        public virtual AppUser owner {get;set;}
        public virtual ICollection<Memory> memories { get; set; }

        //Constructors
        //Basic
        public TimeLine()
        {
            this.memories = new HashSet<Memory>();
        }

        //Detailed
        public TimeLine(string name, string description, string ownerId)
        {
            this.name = name;
            this.description = description;
            this.ownerId = ownerId;
            this.lastModified = DateTime.Now;
        }

        //Ower
        public TimeLine(string name, string description, AppUser owner)
        {
            this.name = name;
            this.description = description;
            this.owner = owner;
            this.lastModified = DateTime.Now;
            this.memories = new HashSet<Memory>();
        }

        //Methods
        public DateTime UpdateLastModified()
        {
            DateTime lastModified = DateTime.Now;

            this.lastModified = lastModified;

            return lastModified;
        }
    }
}
