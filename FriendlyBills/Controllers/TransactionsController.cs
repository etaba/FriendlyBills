using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FriendlyBills.Models;
using FriendlyBills.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;



namespace FriendlyBills.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private TransactionRepository _tranRepo;
        private ApplicationUserManager _userManager;
        private int _groupId;
        private string _memberId;
        private string _userId;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public TransactionsController()
        {
            _tranRepo = new TransactionRepository();
        }

        // GET: Transactions
        public ActionResult Index(string memberId, int groupId)
        {
            //TODO: move to constructor and store this info in a better more global way
            TempData["UserID"] = User.Identity.GetUserId();
            if (groupId != null)
            {
                TempData["GroupID"] = groupId;
            }
            if (memberId != null)
            {
                TempData["MemberID"] = memberId;
            }

            List<Transaction> transactions = _tranRepo.GetTransactions(groupId, User.Identity.GetUserId(), memberId);
            List<TransactionViewModel> transactionRows = new List<TransactionViewModel>();
            foreach (Transaction transaction in transactions)
            {
                TransactionViewModel transactionRow = new TransactionViewModel(transaction);
                transactionRows.Add(transactionRow);
            }
            return View(transactionRows);
        }

        //GET: Transactions
        //public ActionResult Index()
        //{
        //    return RedirectToAction("Index", new { memberId = TempData["MemberID"].ToString(), groupId = (int)TempData["GroupID"] });
        //}

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _tranRepo.GetTransactionByID((int)id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description,MonetaryAmount,AdditionalTimestamp")] TransactionViewModel tran)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction()
                {
                    MonetaryAmount = tran.MonetaryAmount,
                    Description = tran.Description,
                    AdditionalTimestamp = tran.AdditionalTimestamp,

                    EntryTimestamp = DateTime.Now,
                    TargetUserID = TempData["MemberID"].ToString(),
                    SubmitterID = TempData["UserID"].ToString(),
                    GroupID = (int)TempData["GroupID"]
                };
                _tranRepo.AddTransaction(transaction);
                return RedirectToAction("Index", "Transactions", new { memberId = TempData["MemberID"].ToString(), groupId = (int)TempData["GroupID"] });
            }
            return View();
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionViewModel transaction = new TransactionViewModel(_tranRepo.GetTransactionByID((int)id));
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Description,MonetaryAmount,AdditionalTimestamp")] TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                Transaction tran = _tranRepo.GetTransactionByID((int)transaction.ID);
                tran.Description = transaction.Description;
                tran.MonetaryAmount = transaction.MonetaryAmount;
                tran.AdditionalTimestamp = transaction.AdditionalTimestamp;
                _tranRepo.Edit(tran);
                return RedirectToAction("Index","Transactions",new {memberId=TempData["MemberID"].ToString(),groupId=(int)TempData["GroupID"]});
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction tran = _tranRepo.GetTransactionByID((int)id);
            if (tran == null)
            {
                return HttpNotFound();
            }

            if (tran.SubmitterID != TempData["UserID"].ToString()) //do not allow user to delete transactions they didnt submit
            {
                //TODO: some notification here
                return RedirectToAction("Index");
            }

            TransactionViewModel tranViewModel = new TransactionViewModel(tran);
            
            return View(tranViewModel);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _tranRepo.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tranRepo.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
