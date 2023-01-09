using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment2.Models;

namespace Assignment2.Data
{
    public class AssignmentContext : DbContext
    {
        public AssignmentContext (DbContextOptions<AssignmentContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment2.Models.Department> Department {get; set;}
        public DbSet<Assignment2.Models.User> User {get; set;}
        public DbSet<Assignment2.Models.UserAccess> UserAccess {get; set;}
        public DbSet<Assignment2.Models.UserAccessMap> UserAccessMap {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserAccess>().ToTable("UserAccess");
            modelBuilder.Entity<UserAccessMap>().ToTable("UserAccessMap");
        }
    }
}
