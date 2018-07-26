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
        public DateTime lastModified { get; set; }

        //Constructors
        //Basic
        public Memory()
        {

        }

        //Detailed
        public Memory(string name, string description)
        {
            this.name = name;
            this.description = description;
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
