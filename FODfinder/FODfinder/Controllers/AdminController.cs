using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FODfinder.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FODfinder.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<ApplicationUser> users = context.Users.ToList();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            users.RemoveAll(x => userManager.IsInRole(x.Id, "Admin") || userManager.IsInRole(x.Id, "SuperAdmin"));

            return View(users);
        }

        public ActionResult Delete(string id)
        {
            ApplicationUser user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            if (userManager.IsInRole(id, "SuperAdmin") || userManager.IsInRole(id, "Admin"))
            {
                return new HttpUnauthorizedResult();
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
    }
}