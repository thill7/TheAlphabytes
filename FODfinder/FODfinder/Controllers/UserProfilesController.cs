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

namespace FODfinder.Controllers
{
    public class UserProfilesController : Controller
    {
        private FFDBContext db = new FFDBContext();

        // GET: UserProfiles
        [HttpGet]
        public ActionResult Search()
        {
            string search = Request.QueryString["search"];
            if (!String.IsNullOrEmpty(search))
            {
                var matchLastName = db.UserInformations.Where(ui => ui.lastName.Contains(search));
                var queryResults = db.UserInformations.Where(ui => ui.firstName.Contains(search)).Union(matchLastName);
                List<UserInformation> resultList = queryResults.Where(p => db.UserInformations.Any(a => a.userID == p.userID)).ToList();
                ViewBag.Success = true;
                if (!resultList.Any())
                {
                    ViewBag.Success = false;
                }
                return View(resultList);
            }
            //var userProfiles = db.UserProfiles.Include(u => u.AspNetUser);
            return View();
        }

        // GET: UserProfiles/Details/5
        public ActionResult UserProfile(string id)
        {
            string userID = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            List<UserList> userLists = db.UserLists.Where(u => u.userID == id).ToList();
            ViewBag.UserLists = userLists;
            if (userLists.Any())
            {
                ViewBag.anyLists = true;
            }
            else
            {
                ViewBag.anyLists = false;
            }
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            if (userProfile.is_public || userProfile.userID == userID) {

                string fname = db.UserInformations.Where(u=>u.userID ==id).ToList().Select(u => u.firstName).Single();
                string lname = db.UserInformations.Where(u=>u.userID ==id).ToList().Select(u => u.lastName).Single();
                ViewBag.Name = fname + " " + lname;
                ViewBag.Contact = db.AspNetUsers.Where(u => u.Id == id).ToList().Select(u => u.Email).Single();
                ViewBag.Gender = db.UserInformations.Where(u=>u.userID ==id).ToList().Select(u => u.gender).Single();
                ViewBag.Location = db.UserInformations.Where(u=>u.userID ==id).ToList().Select(u => u.country).Single();
                ViewBag.Ethnicity = db.UserInformations.Where(u=>u.userID ==id).ToList().Select(u => u.ethnicity).Single();
                DateTime birthdate = db.UserInformations.Where(u=>u.userID ==id).Select(u => u.birthdate).FirstOrDefault();
                DateTime today = DateTime.Now.Date;
                ViewBag.Age = today.Year - birthdate.Year;
                ViewBag.ProfilePic = db.UserProfiles.Where(u => u.userID == id).ToList().Select(u => u.profileImgUrl).Single() + ".jpg";
                if(userID == id)
                {
                    ViewBag.ownsProfile = "true";
                }
                else
                {
                    ViewBag.ownsProfile = "false";
                }
                return View(userProfile);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        // GET: UserProfiles/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,is_public,optIn,showEthnicity,showAge,showCountry,showGender,description,profileImgUrl")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", userProfile.userID);
            return View(userProfile);
        }

        // GET: UserProfiles/Edit/5
        public ActionResult Edit(string id)
        {
            string userID = User.Identity.GetUserId();
            if (id == null || userID != id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            UserInformation userInformation = db.UserInformations.Find(id);
            EditProfileViewModel userEditProfile = new EditProfileViewModel(userInformation, userProfile);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", userProfile.userID);
            return View(userEditProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,is_public,optIn,showEthnicity,showAge,showCountry,showGender,description,profileImgUrl")] UserProfile userProfile, [Bind(Include = "userID,firstName,lastName,ethnicity,birthdate,country,gender,")] UserInformation userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile", new { id = userProfile.userID });
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", userProfile.userID);
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
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
