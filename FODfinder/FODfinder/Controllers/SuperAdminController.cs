using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FODfinder.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FODfinder.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<ApplicationUser> users = context.Users.ToList();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            users.RemoveAll(x => userManager.IsInRole(x.Id, "SuperAdmin"));

            return View(users);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            ApplicationUser user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (userManager.IsInRole(id, "SuperAdmin"))
            {
                return new HttpUnauthorizedResult("SuperAdmin cannot be removed");
            }
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
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
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

        public ActionResult Demote(string id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            ApplicationUser user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Demote(ApplicationUser userToDemote)
        {
            ApplicationUser user = context.Users.Where(x => x.Id == userToDemote.Id).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            IdentityResult result = userManager.RemoveFromRole(userToDemote.Id, "Admin");
            return RedirectToAction("Index");
        }
    }
}