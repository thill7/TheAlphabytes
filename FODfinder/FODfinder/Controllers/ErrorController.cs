using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FODfinder.Models;

namespace FODfinder.Controllers
{
    public class ErrorController : Controller
    {
        private FFDBContext db = new FFDBContext();

        public ActionResult Default()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            Random rand = new Random();
            int quoteID = rand.Next(1, db.Quotes.Count() + 1);
            Quote quote = db.Quotes.FirstOrDefault(x => x.ID == quoteID);
            Response.StatusCode = 404;
            return View(quote);
        }

        public ActionResult InternalServer()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}