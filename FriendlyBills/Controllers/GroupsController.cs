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
using Microsoft.Owin.Security;
using FriendlyBills.Models;

namespace FriendlyBills.Controllers
{
    public class GroupsController : Controller
    {
        private IRepository<Group> _groupRepo = new GroupRepository();
        private IRepository<GroupMembership> _groupMemRepo = new GroupMembershipRepository();
        private ApplicationUserManager _userManager;

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
        // GET: Groups
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser user = UserManager.FindById(userId);
            //List< GroupViewModel> groupList = new List<GroupViewModel>();
            //groupList = _groupRepo.List.Select(x => new GroupViewModel(x)).ToList();
            List<Group> gList = _groupRepo.GetGroupsByUser(user);
            foreach (Group g in gList)
            {
                CreateGroupViewModel grp = new CreateGroupViewModel(g);
                //groupList.Add(grp);
            }
            return View();
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = _groupRepo.Find((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }
            CreateGroupViewModel groupViewModel = new CreateGroupViewModel(group);
            return View(groupViewModel);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] CreateGroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                //ApplicationUser user = UserManager.FindById(userId);

                Group grp = new Group() 
                {
                    Name = group.Name,  
                    Description = "test group"
                };
                int grpId = _groupRepo.Add(grp);

                GroupMembership grpMem = new GroupMembership() 
                {
                    GroupID = grpId, 
                    UserID = userId, 
                    Rank = 1
                };
                _groupMemRepo.Add(grpMem);

                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = _groupRepo.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            CreateGroupViewModel groupViewModel = new CreateGroupViewModel(group);
            return View(groupViewModel);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] CreateGroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                //_groupRepo.Update(new Group(group));
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = _groupRepo.Find((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }
            CreateGroupViewModel groupViewModel = new CreateGroupViewModel(group);
            return View(groupViewModel);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _groupRepo.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _groupRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
