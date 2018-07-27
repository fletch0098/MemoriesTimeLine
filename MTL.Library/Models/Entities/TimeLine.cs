using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MTL.Library.Models.Entities
{
    public class TimeLine
    {
        //Properties
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public AppUser owner { get; set; }
        public DateTime lastModified { get; set; }

        //Constructors
        //Basic
        public TimeLine()
        {

        }

        //Detailed
        public TimeLine(string name, string description, AppUser owner)
        {
            this.name = name;
            this.description = description;
            this.owner = owner;
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
