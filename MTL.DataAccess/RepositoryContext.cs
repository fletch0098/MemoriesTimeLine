using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MTL.DataAccess.Entities;

namespace MTL.DataAccess
{
    public class RepositoryContext : IdentityDbContext
    {
        public RepositoryContext(DbContextOptions opts)
        : base(opts)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Memory>().ToTable("Memories");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TimeLine> TimeLines { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }


    }
}
