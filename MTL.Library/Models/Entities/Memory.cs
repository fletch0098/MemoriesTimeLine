using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MTL.Library.Models.Entities
{
    public class Memory
    {
        //Properties
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public int timeLineId { get; set; }
        public DateTime lastModified { get; set; }

        public virtual TimeLine timeLine { get; set; }

        //Constructors
        //Basic
        public Memory()
        {

        }

        //Detailed
        public Memory(string name, string description, DateTime date, int timeLineId)
        {
            this.name = name;
            this.description = description;
            this.date = date;
            this.timeLineId = timeLineId;
            this.lastModified = DateTime.Now;
        }

        //TimeLine
        public Memory(string name, string description, DateTime date, TimeLine timeLine)
        {
            this.name = name;
            this.description = description;
            this.date = date;
            this.timeLine = timeLine;
            this.lastModified = DateTime.Now;
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
