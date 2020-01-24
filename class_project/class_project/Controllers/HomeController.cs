using class_project.DAL;
using class_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace class_project.Controllers
{
    public class HomeController : Controller
    {
        private CPDBContext db = new CPDBContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search()
        {
            string search = Request.QueryString["search"];
            if (!String.IsNullOrEmpty(search))
            {
                var matchLastName = db.People.Where(p => p.LastName.Contains(search));
                var query = db.People.Where(p => p.FirstName.Contains(search)).Union(matchLastName);
                List<Person> SearchList = query.ToList();
                ViewBag.Success = true;
                if (!SearchList.Any())
                {
                    ViewBag.Success = false;
                }
                return View(SearchList);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}