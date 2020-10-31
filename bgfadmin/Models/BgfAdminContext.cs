using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bgfadmin.Models
{

    public class BgfAdminContext : IdentityDbContext<User>
    {
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Sex> Sex { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TreeAdmin> TreeAdmin { get; set; }
        public DbSet<Profile_TreeAdmin> Profile_TreeAdmin { get; set; }

        public BgfAdminContext(DbContextOptions<BgfAdminContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //modelBuilder.Entity<Sex>().HasKey(new string[] { "Id" });
            //modelBuilder.Entity<Profile>().HasKey(new string[] { "Id" });
            //modelBuilder.Entity<TreeAdmin>().HasKey(new string[] { "Id" });
            modelBuilder.Entity<Profile_TreeAdmin>().HasKey(new string[] { "ProfileId", "TreeId" });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}