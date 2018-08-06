using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MTL.DataAccess.Contracts;

namespace MTL.DataAccess.Entities
{
    public class BaseEntity : IEntity
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
