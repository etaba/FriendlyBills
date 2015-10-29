using FriendlyBills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendlyBills.DAL
{
    public class IEntity
    {
        public string Id;
    }

    public interface IRepository<T> where T: IEntity
    {
        IEnumerable<T> List { get; }
        List<T> GetGroupsByUser(ApplicationUser user);
        int Add(T entity);
        void Delete(T entity);
        void Delete(int Id);
        void Update(T entity);
        T Find(int Id);
        void Dispose();
    }

    public class GroupRepository : IRepository<Group>
    {
        private ApplicationDbContext _db;

        public GroupRepository()
        {
            _db = new ApplicationDbContext();
        }

        public IEnumerable<Group> List
        {
            get
            {
                return _db.Groups;
            }
        }
        public List<Group> GetGroupsByUser(ApplicationUser user)
        {
            return (from gm in _db.GroupMemberships
                 join g in _db.Groups
                 on gm.GroupID equals g.ID
                 where gm.User == user
                 select g).ToList();
        }

        public int Add(Group newGroup)
        {
            newGroup = _db.Groups.Add(newGroup);
            _db.SaveChanges();
            return newGroup.ID;
        }

        public void Delete(Group remGroup)
        {
            _db.Groups.Remove(remGroup);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Groups.Remove(Find(id));
            _db.SaveChanges();
        }

        public void Update(Group upGroup)
        {
            _db.Entry(upGroup).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

        public Group Find(int Id)
        {
            var result = (from g in _db.Groups
                          where g.ID == Id
                          select g).FirstOrDefault();
            return result;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }

    public class GroupMembershipRepository : IRepository<GroupMembership>
    {
        private ApplicationDbContext _db;

        public GroupMembershipRepository()
        {
            _db = new ApplicationDbContext();
        }

        public IEnumerable<GroupMembership> List
        {
            get
            {
                return _db.GroupMemberships.ToList();
            }
        }
        public List<GroupMembership> GetGroupsById(ApplicationUser user)
        {
            return null;
        }

        public int Add(GroupMembership newGroupMembership)
        {
            newGroupMembership = _db.GroupMemberships.Add(newGroupMembership);
            _db.SaveChanges();
            return newGroupMembership.ID;
        }

        public void Delete(GroupMembership remGroupMembership)
        {
            _db.GroupMemberships.Remove(remGroupMembership);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.GroupMemberships.Remove(Find(id));
            _db.SaveChanges();
        }

        public void Update(GroupMembership upGroupMembership)
        {
            _db.Entry(upGroupMembership).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

        public GroupMembership Find(int Id)
        {
            var result = (from g in _db.GroupMemberships
                          where g.ID == Id
                          select g).FirstOrDefault();
            return result;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
        
    }
}