using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Entities
{
    /// 
    /// A Timeline about your life´s trails
    /// 
    [Table("TimeLines")]
    public class TimeLine : BaseEntity
    {
        /// 
        /// ## Name - Remarks ## 
        /// The name of API, used as an identifier
        /// 
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// A breif description of the timeline
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Description { get; set; }

        public String IdentityId { get; set; }

        public TimeLine()
        {
            this.LastModified = DateTime.Now;
        }

        //Detailed
        public TimeLine(string name, string description, string identityId)
        {
            this.Name = name;
            this.Description = description;
            this.IdentityId = identityId;
            this.LastModified = DateTime.Now;
        }

        //With Owner
        public TimeLine(string name, string description, AppUser identity)
        {
            this.Name = name;
            this.Description = description;
            this.IdentityId = identity.Id;
            this.LastModified = DateTime.Now;
        }

    }
}
