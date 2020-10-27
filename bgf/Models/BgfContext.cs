using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bgf.Models
{

    public class BgfContext : IdentityDbContext<Student>
    {
        public BgfContext(DbContextOptions<BgfContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            /*
             modelBuilder.Entity<Student>(b =>
            {
                b.ToTable("Student");
            });
            
            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("StudentRole");
            });
            modelBuilder.Entity<IdentityRole<string>>(b =>
            {
                b.ToTable("StudentIdentityRole");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("StudentIdentityUserClaim");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("StudentIdentityUserLogin");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("StudentIdentityUserToken");
            });
            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("StudentIdentityRole");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("StudentIdentityRoleClaim");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("StudentIdentityUserRole");
            });*/

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName == "AspNetUsers")
                    entityType.SetTableName("Student");
                else if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName("Student" + tableName.Substring(6));
                }


                //https://stackoverflow.com/questions/41442513/how-can-i-change-default-asp-net-identity-table-names-in-net-core/41442942
                //Add-Migration
                //Update-database

            }

            // public DbSet<TodoItem> TodoItem { get; set; }
        }


    }
}