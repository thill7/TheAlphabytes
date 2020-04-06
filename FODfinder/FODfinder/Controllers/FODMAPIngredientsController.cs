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
    public class FODMAPIngredientsController : Controller
    {
        private FFDBContext db = new FFDBContext();

        public ActionResult Browse()
        {
            return View(db.FODMAPIngredients.ToList());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ContentResult Create(string assignLabel, string ingredientName)
        {
            string userID = User.Identity.GetUserId();
            if (userID != null)
            {
                SavedFood savedFood = new SavedFood(usdaFoodID, userID, brandOwner, upc, description);
                try
                {
                    db.SavedFoods.Add(savedFood);
                    db.SaveChanges();
                    var jsonData = new
                    {
                        success = true,
                        message = "Food has been saved.",
                        redirect = false
                    };

                    var result = JObject.FromObject(jsonData);
                    return Content(result.ToString(), "Application/json");
                }
                catch
                {
                    var jsonData2 = new
                    {
                        success = false,
                        message = "Food has already been saved.",
                        redirect = false
                    };

                    var result2 = JObject.FromObject(jsonData2);
                    return Content(result2.ToString(), "Application/json");
                }
            }

            var jsonData3 = new
            {
                success = false,
                message = "User not logged in.",
                redirect = true
            };

            var result3 = JObject.FromObject(jsonData3);
            return Content(result3.ToString(), "Application/json");
        }
        /*
        [HttpGet]
        public ActionResult Search()
        {
            string search = Request.QueryString["search"];
            if (!String.IsNullOrEmpty(search))
            {
                var matchAlias = db.FODMAPIngredients.Where(p => p.Aliases.Contains(search));
                List<FODMAPIngredient> allMatches = db.FODMAPIngredients.Where(p => p.Name.Contains(search)).Union(matchAlias).ToList();
                ViewBag.Success = true;
                if (!allMatches.Any())
                {
                    ViewBag.Success = false;
                }
                return View(allMatches);
            }
            return View();
        }
        */

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
