using System;
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

        public void Modified()
        {
            DateTime lastModified = DateTime.Now;
            this.LastModified = lastModified;
        }
    }
}
