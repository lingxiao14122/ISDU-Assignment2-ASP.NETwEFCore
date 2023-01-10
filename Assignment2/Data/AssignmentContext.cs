﻿using Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Data
{
    public class AssignmentContext : DbContext
    {
        public AssignmentContext(DbContextOptions<AssignmentContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment2.Models.Department> Departments { get; set; }
        public DbSet<Assignment2.Models.User> Users { get; set; }
        public DbSet<Assignment2.Models.UserAccess> UserAccess { get; set; }
        public DbSet<Assignment2.Models.UserAccessMap> UserAccessMap { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<User>()
                .ToTable("User")
                .Property(u => u.DateEntered)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<UserAccess>().ToTable("UserAccess");
            modelBuilder.Entity<UserAccessMap>().ToTable("UserAccessMap");
        }
    }
}
