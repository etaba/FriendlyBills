using FriendlyBills.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FriendlyBills.DAL
{
    //public class FriendlyBillsDatabaseContext : DbContext
    //{
    //    public FriendlyBillsDatabaseContext() : base("FriendlyBillsDatabaseContext") { }

    //    public DbSet<Group> Groups { get; set; }
    //    public DbSet<GroupMembership> GroupMemberships { get; set; }
    //    public DbSet<Transaction> Transactions { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder); 
    //        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    //    }
    //} 
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() : base ("FriendlyBillsDatabaseContext")
        { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Identity and Authorization
        public DbSet<IdentityUserLogin> UserLogins { get; set; }
        public DbSet<IdentityUserClaim> UserClaims { get; set; }
        public DbSet<IdentityUserRole> UserRoles { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}