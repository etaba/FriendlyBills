using FriendlyBills.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FriendlyBills.DAL
{
    public class TransactionRepository
    {
        internal ApplicationDbContext context;

        public TransactionRepository()
        {
            this.context = new ApplicationDbContext();
        }

        public List<Transaction> GetTransactions(int grpId,
                                                 string memberId1, 
                                                 string memberId2)
        {
            return context.Transactions.Where(t => t.GroupID == grpId && 
                                                   ((t.SubmitterID == memberId1 && t.TargetUserID == memberId2) ||
                                                   (t.SubmitterID == memberId2 && t.TargetUserID == memberId1))).ToList();
        }

        public decimal GetAccountBalance(int grpId,
                                         string userId, 
                                         string memberId)
        {
            return context.Transactions.Where(t => t.GroupID == grpId &&
                                                    ((t.SubmitterID == userId && t.TargetUserID == memberId) ||
                                                    (t.SubmitterID == memberId && t.TargetUserID == userId))).Sum(t => (int?)t.MonetaryAmount) ?? 0;

        }

        public void AddTransaction(Transaction transaction)
        {
            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public Transaction GetTransactionByID(int id)
        {
            return context.Transactions.Where(t => t.ID == id).FirstOrDefault();
        }

        public void Edit(Transaction tran)
        {
            context.Entry(tran).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Transaction transaction = context.Transactions.Find(id);
            context.Transactions.Remove(transaction);
            context.SaveChanges();
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