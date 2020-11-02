using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bgfadmin.Models
{
    public static class Globals
    {
        public static Profile[] Profile = null;
        public static TreeAdmin[] Tree = null;
        public static Hashtable profileTree = null; // ((ArrayList) profileTree[1]).Contains(3);

        public static void FillGlobals(BgfAdminContext context)
        {
            Globals.Profile = context.Profile.Where(s => s.Id < 10).ToArray();
            Globals.Tree = context.TreeAdmin.ToArray();

            Globals.profileTree = new Hashtable();
            var ptarray = context.Profile_TreeAdmin.ToArray();
            
            foreach(Profile profile in Profile)
            {
                ArrayList treeIds = new ArrayList();
                foreach (var pt in ptarray)
                {
                    if (pt.ProfileId == profile.Id)
                        treeIds.Add(pt.TreeId);
                }
                if(treeIds.Count > 0)
                    profileTree.Add(profile.Id, treeIds);
            }
            
        }
        public static TreeAdmin[] GetProfileTree( int profileId)
        {
            if (profileId == 2)
                return Tree;
            if (!profileTree.Contains(profileId)) return null;
            return Tree.Where(t => ((ArrayList)profileTree[profileId]).Contains(t.Id)).ToArray();
        }

    }

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