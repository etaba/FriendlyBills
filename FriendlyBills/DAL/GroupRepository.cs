using FriendlyBills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendlyBills.DAL
{
    public class GroupRepository
    {
        internal ApplicationDbContext context;
        
        public GroupRepository()
        {
            this.context = new ApplicationDbContext();
        }
        
        public List<Group> GetGroupsByUser(string userId)
        {
            return (from gm in context.GroupMemberships
                    join g in context.Groups
                    on gm.GroupID equals g.ID
                    where gm.UserID == userId
                    select g).ToList();
        }

        public void CreateGroup(Group grp,
                                string userId)
        {
            grp = context.Groups.Add(grp);
            GroupMembership grpMem = new GroupMembership()
            {
                GroupID = grp.ID,
                UserID = userId,
                Rank = 1
            };
            context.GroupMemberships.Add(grpMem);
            context.SaveChanges();
        }
        
        public bool JoinGroup(int grpID,
                              int userID)
        {
            if (context.Groups.Find(grpID) == null)
            {
                return false;
            }
            else
            {
                GroupMembership grpMem = new GroupMembership()
                {
                    GroupID = grpID,
                    UserID =s userID,
                    Rank = 2
                };
                context.GroupMemberships.Add(grpMem);
                context.SaveChanges();
                return true;
            }
        }
    }
}