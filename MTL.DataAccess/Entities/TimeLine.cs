using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MTL.DataAccess.Entities
{
    [Table("TimeLines")]
    public class TimeLine : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Description { get; set; }

        public int? AppUserId { get; set; }

        public TimeLine()
        {
            this.LastModified = DateTime.Now;
        }

        //Detailed
        public TimeLine(string name, string description, int appUserId)
        {
            this.Name = name;
            this.Description = description;
            this.AppUserId = appUserId;
            this.LastModified = DateTime.Now;
        }

        //Detailed with AppUser
        public TimeLine(string name, string description, AppUser appUser)
        {
            this.Name = name;
            this.Description = description;
            this.AppUserId = appUser.Id;
            this.LastModified = DateTime.Now;
        }

    }
}
