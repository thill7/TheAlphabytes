using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FODfinder.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FODfinder.Controllers
{
    public class HighRiskLabelledIngredientsController : Controller
    {
        private FFDBContext db = new FFDBContext();

        public List<HighRiskLabelledIngredient> RetrieveRiskyIngredients()
        {
            using (FFDBContext db = new FFDBContext())
            {
                List<HighRiskLabelledIngredient> highRiskLabelledIngredientList = new List<HighRiskLabelledIngredient>();
                List<int> highRiskList = db.UserIngredients.Where(u => u.Label == "High-Risk").Select(u => u.LabelledIngredientID).Distinct().ToList();
                foreach (int ingredient in highRiskList)
                {
                    LabelledIngredient labelIng = db.LabelledIngredients.Where(l => l.ID == ingredient).FirstOrDefault();
                    int count = db.UserIngredients.Where(u => u.LabelledIngredientID == ingredient && u.Label == "High-Risk").Count();
                    HighRiskLabelledIngredient newIngredient = new HighRiskLabelledIngredient(labelIng, count);
                    highRiskLabelledIngredientList.Add(newIngredient);
                }
                highRiskLabelledIngredientList = highRiskLabelledIngredientList.OrderByDescending(o => o.countOfLabelOccurences).ToList();
                return highRiskLabelledIngredientList;
            }
        }

        // GET: HighRiskLabelledIngredients
        public ActionResult Index()
        {
            var highRiskLabelledIngredientList = RetrieveRiskyIngredients();
            return View(highRiskLabelledIngredientList);
        }

        public ActionResult Trends()
        {
            return View();
        }

        public JsonResult GetIngredientsForPlot()
        {
            var highRiskLabelledIngredientList = RetrieveRiskyIngredients();
            return Json(highRiskLabelledIngredientList, JsonRequestBehavior.AllowGet);
        }
    }
}
