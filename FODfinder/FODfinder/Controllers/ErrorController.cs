using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FODfinder.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Default()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            var quotes = new List<(string Quote, string Author)>
            {
                ("I swear I'll branch. Promise.", "Tanner"),
                ("All Scrum, no master.", "Tanner"),
            };

            ViewBag.Quotes = quotes;
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult InternalServer()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}