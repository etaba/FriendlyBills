using FriendlyBills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendlyBills.DAL
{
    public class GroupRepository : GenericRepository<Group>
    {
        //public GroupRepository(ApplicationDbContext context) : base(context) {}
        public List<Group> GetGroupsByUser(ApplicationUser user)
        {
            return (from gm in context.GroupMemberships
                    join g in context.Groups
                    on gm.GroupID equals g.ID
                    where gm.UserID == user.Id
                    select g).ToList();
        }

        public void CreateGroup(Group grp,
                                ApplicationUser user)
        {
            grp = context.Groups.Add(grp);
            GroupMembership grpMem = new GroupMembership()
            {
                GroupID = grp.ID,
                UserID = user.Id,
                Rank = 1
            };
            context.GroupMemberships.Add(grpMem);
            context.SaveChanges();
        }
    }
}