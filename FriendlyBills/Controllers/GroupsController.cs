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
    [Authorize]
    public class GroupsController : Controller
    {
        private ApplicationUserManager _userManager;
        private GroupRepository _groupRepo = new GroupRepository();
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
            List<Group> gList = _groupRepo.GetGroupsByUser(userId);
            List<GroupViewModel> gViewModel = new List<GroupViewModel>();
            foreach(Group g in gList)
            {
                Dictionary<string, decimal> memberBalances = _groupRepo.GetMemberBalances(g.ID, userId);
                List<object> memberDetails = _groupRepo.GetMemberDetails(g.ID, userId);
                gViewModel.Add(new GroupViewModel(g, memberBalances));
            }
            return View(gViewModel);
        }

        //// GET: Groups/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Group group = _groupRepo.Find((int)id);
        //    if (group == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    CreateGroupViewModel groupViewModel = new CreateGroupViewModel(group);
        //    return View(groupViewModel);
        //}

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
        public ActionResult Create([Bind(Include = "Name,Description")] GroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();

                Group grp = new Group() 
                {
                    Name = group.Name,  
                    Description = group.Description
                };
                _groupRepo.CreateGroup(grp, userId);

                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Join
        public ActionResult Join()
        {
            return View();
        }

        // POST: Groups/Join
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join([Bind(Include = "Name,ID")] GroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();

                _groupRepo.JoinGroup(group.Id, userId);

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
            Group group = _groupRepo.GetGroupByID(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            GroupViewModel groupViewModel = new GroupViewModel(group, _groupRepo.GetMemberBalances(group.ID,User.Identity.GetUserId()));
            return View(groupViewModel);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Description,Name")] GroupViewModel group)
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
            _groupRepo.Delete((int)id);
            return View();
        } 

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _groupRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
