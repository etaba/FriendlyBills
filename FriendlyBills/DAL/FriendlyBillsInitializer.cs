using FriendlyBills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FriendlyBills.DAL
{
    public class FriendlyBillsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var groups = new List<Group>
            {
                new Group{Name="group_0",Description="description for group 0"},
                new Group{Name="group_1",Description="description for group 1"}
            };

            groups.ForEach(g => context.Groups.Add(g));
            context.SaveChanges();

            //var groupMems = new List<GroupMembership>
            //{
            //    new GroupMembership{}
            //};
            base.Seed(context);
        }
    }
}