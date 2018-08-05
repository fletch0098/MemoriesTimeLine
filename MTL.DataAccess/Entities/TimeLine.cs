﻿using System;
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

        public String OwnerId { get; set; }

        public TimeLine()
        {

        }

        //Detailed
        public TimeLine(string name, string description, string ownerId)
        {
            this.Name = name;
            this.Description = description;
            this.OwnerId = ownerId;
            this.LastModified = DateTime.Now;
        }

        //With Owner
        public TimeLine(string name, string description, AppUser owner)
        {
            this.Name = name;
            this.Description = description;
            this.OwnerId = owner.Id;
            this.LastModified = DateTime.Now;
        }

    }
}