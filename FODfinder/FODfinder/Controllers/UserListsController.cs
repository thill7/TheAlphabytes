using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FODfinder.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace FODfinder.Controllers
{
    [Authorize]
    public class UserListsController : Controller
    {
        private FFDBContext db = new FFDBContext();

        // GET: UserLists
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            return View(db.UserLists.Where(x => x.userID == userID).ToList());
        }

        [HttpGet]
        public ContentResult getLists()
        {
            string userID = User.Identity.GetUserId();
            if (userID != null)
            {
                List<UserList> userLists = db.UserLists.Where(x => x.userID == userID).ToList();
                if (!userLists.Any())
                {
                    var jsonNoLists = new
                    {
                        success = false,
                        redirect = false,
                    };

                    var resultNoLists = JObject.FromObject(jsonNoLists);
                    return Content(resultNoLists.ToString(), "Application/json");
                }

                var jsonLoggedIn = new
                {
                    success = true,
                    redirect = false,
                    lists = userLists
                };

                var resultSuccess = JObject.FromObject(jsonLoggedIn);
                return Content(resultSuccess.ToString(), "Application/json");
            }

            var jsonNotLoggedIn = new
            {
                success = false,
                redirect = true
            };

            var resultNotLoggedIn = JObject.FromObject(jsonNotLoggedIn);
            return Content(resultNotLoggedIn.ToString(), "Application/json");
        }

        // GET: UserLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserList userList = db.UserLists.Find(id);
            if (userList == null)
            {
                return HttpNotFound();
            }
            return View(userList);
        }

        // GET: UserLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "listID,userID,listName")] UserList userList)
        {
            userList.userID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.UserLists.Add(userList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userList);
        }

        // GET: UserLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserList userList = db.UserLists.Find(id);
            if (userList == null)
            {
                return HttpNotFound();
            }
            return View(userList);
        }

        // POST: UserLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "listID,userID,listName")] UserList userList)
        {
            userList.userID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(userList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userList);
        }

        // GET: UserLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserList userList = db.UserLists.Find(id);
            if (userList == null)
            {
                return HttpNotFound();
            }
            return View(userList);
        }

        // POST: UserLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserList userList = db.UserLists.Find(id);
            db.UserLists.Remove(userList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
