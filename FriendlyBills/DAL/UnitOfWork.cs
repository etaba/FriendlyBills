using System;
using FriendlyBills.Models;

namespace FriendlyBills.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Group> groupRepository;
        private GenericRepository<GroupMembership> groupMemRepository;

        public GenericRepository<Group> GroupRepository
        {
            get
            {

                if (this.groupRepository == null)
                {
                    this.groupRepository = new GenericRepository<Group>(context);
                }
                return groupRepository;
            }
        }

        public GenericRepository<GroupMembership> GroupMembershipRepository
        {
            get
            {

                if (this.groupMemRepository == null)
                {
                    this.groupMemRepository = new GenericRepository<GroupMembership>(context);
                }
                return groupMemRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}