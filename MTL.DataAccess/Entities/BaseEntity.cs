using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime LastModified { get; set; }


        //Methods
        public void Modified()
        {
            DateTime lastModified = DateTime.Now;
            this.LastModified = lastModified;
        }
    }
}
