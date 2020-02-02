using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using class_project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace class_project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<ApplicationUser> users = context.Users.ToList();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            users.RemoveAll(x => userManager.IsInRole(x.Id, "Admin"));

            return View(users);
        }

        public ActionResult Delete(string id)
        {
            ApplicationUser user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(ApplicationUser userToDelete)
        {
            ApplicationUser user = context.Users.Where(x => x.Id == userToDelete.Id).FirstOrDefault();
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Promote(string id)
        {
            ApplicationUser user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Promote(ApplicationUser userToPromote)
        {
            ApplicationUser user = context.Users.Where(x => x.Id == userToPromote.Id).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            IdentityResult result = userManager.AddToRole(userToPromote.Id, "Admin");
            return RedirectToAction("Index");
        }
    }
}