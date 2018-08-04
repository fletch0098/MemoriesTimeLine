using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    [Table("Memories")]
    public class Memory : BaseEntity
    {
        //Properties
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        public int TimeLineId { get; set; }

        public virtual TimeLine TimeLine { get; set; }

        //Constructors
        //Basic
        public Memory()
        {

        }

        //Detailed
        public Memory(string name, string description, DateTime date, int timeLineId)
        {
            this.Name = name;
            this.Description = description;
            this.Date = date;
            this.TimeLineId = timeLineId;
            this.LastModified = DateTime.Now;
        }

        //TimeLine
        public Memory(string name, string description, DateTime date, TimeLine timeLine)
        {
            this.Name = name;
            this.Description = description;
            this.Date = date;
            this.TimeLine = timeLine;
            this.LastModified = DateTime.Now;
        }
    }
}
