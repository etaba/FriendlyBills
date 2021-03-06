﻿using FriendlyBills.Models;
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

        public Group GetGroupByID(int ID)
        {
            return context.Groups.Find(ID);
        }

        public void Delete(int ID)
        {
            Group grp = GetGroupByID(ID);
            context.Groups.Remove(grp);
            context.SaveChanges();
        }
        
        public List<Group> GetGroupsByUser(string userId)
        {
            return (from gm in context.GroupMemberships
                    join g in context.Groups
                    on gm.GroupID equals g.ID
                    where gm.UserID == userId
                    select g).ToList();
        }
        
        public List<ApplicationUser> GetUsersByGroup(int grpId)
        {
            return (from gm in context.GroupMemberships
                    join u in context.Users
                    on gm.UserID equals u.Id
                    where gm.GroupID == grpId
                    select u).ToList();
        }

        //public Dictionary<string, decimal> GetMemberBalances(int grpId, 
        //                                                     string currUserId)
        //{
        //    List<ApplicationUser> users = GetUsersByGroup(grpId).Where(u => u.Id != currUserId).ToList();
        //    Dictionary<string, decimal> groupDetails = new Dictionary<string, decimal>();
        //    foreach (ApplicationUser user in users)
        //    {
        //            decimal sum = context.Transactions.Where(t => t.GroupID == grpId &&
        //                                                    ((t.SubmitterID == user.Id && t.TargetID == currUserId) ||
        //                                                    (t.SubmitterID == currUserId && t.TargetID == user.Id))).Sum(t => (int?)t.MonetaryAmount) ?? 0;
        //            groupDetails.Add(Utilities.FullName(user), sum);
        //    }
        //    return groupDetails;
        //}
        
        public List<MemberDetail> GetMemberDetails(int grpId, 
                                             string currUserId)
        {
            List<ApplicationUser> users = GetUsersByGroup(grpId).Where(u => u.Id != currUserId).ToList();
            List<MemberDetail> memberDetails = new List<MemberDetail>();
            foreach (ApplicationUser user in users)
            {
                decimal sum = context.Transactions.Where(t => t.GroupID == grpId &&
                                                        ((t.SubmitterID == user.Id && t.TargetUserID == currUserId) ||
                                                        (t.SubmitterID == currUserId && t.TargetUserID == user.Id))).Sum(t => (int?)t.MonetaryAmount) ?? 0;

                MemberDetail memberDetail = new MemberDetail
                {
                    UserID = user.Id,
                    FullName = Utilities.FullName(user),
                    Balance = sum
                };
                memberDetails.Add(memberDetail);
            }
            return memberDetails;
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
        
        public bool JoinGroup(int grpID,
                              string userID)
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
                    UserID = userID,
                    Rank = 2
                };
                GroupMembership grpMem2 = context.GroupMemberships.Add(grpMem);
                context.SaveChanges();
                return true;
            }
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}