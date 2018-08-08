using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    [Table("AppUsers")]
    public class AppUser : BaseEntity
    {
        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(60, ErrorMessage = "FirstName can't be longer than 60 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(60, ErrorMessage = "LastName can't be longer than 60 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Identity is required")]
        public string IdentityId { get; set; }

        //Constructors
        //Basic
        public AppUser()
        {
            this.Modified();
        }

        public void LastLoggedOn()
        {
            this.Modified();
        }
    }
}
