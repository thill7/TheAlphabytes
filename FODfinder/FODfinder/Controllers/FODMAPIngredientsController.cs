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
                int ingredientID = db.LabelledIngredients.Where(s => s.Name == ingredientName).Select(s => s.ID).FirstOrDefault();
                if (ingredientID == 0)
                {
                    LabelledIngredient labelIngredient = new LabelledIngredient();
                    labelIngredient.Name = ingredientName;
                    try
                    {
                        db.LabelledIngredients.Add(labelIngredient);
                        db.SaveChanges();
                        ingredientID = labelIngredient.ID;
                    }
                    catch
                    {
                        var jsonData_failed_ingredient_add = new
                        {
                            success = false,
                            message = "Ingredient not added to database",
                            redirect = false
                        };

                        var result = JObject.FromObject(jsonData_failed_ingredient_add);
                        return Content(result.ToString(), "Application/json");
                    }
                }
                if(db.UserIngredients.Where(s => s.LabelledIngredientID == ingredientID && s.userID == userID).Count() > 0)
                {
                    //change current record
                }
                else
                {
                    UserIngredient userIng = new UserIngredient(userID, assignLabel, ingredientID);
                    try
                    {
                        db.UserIngredients.Add(userIng);
                        db.SaveChanges();
                        var jsonData_success = new
                        {
                            success = true,
                            message = "Label has been saved.",
                            redirect = false
                        };

                        var result = JObject.FromObject(jsonData_success);
                        return Content(result.ToString(), "Application/json");
                    } 
                    catch
                    {
                        var jsonData_fail = new
                        {
                            success = false,
                            message = "Something went wrong",
                            redirect = false
                        };

                        var result2 = JObject.FromObject(jsonData_fail);
                        return Content(result2.ToString(), "Application/json");
                    }
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
