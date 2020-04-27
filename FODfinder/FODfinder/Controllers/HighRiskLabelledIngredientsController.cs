using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FODfinder.Models;

namespace FODfinder.Controllers
{
    public class HighRiskLabelledIngredientsController : Controller
    {
        private FFDBContext db = new FFDBContext();

        // GET: HighRiskLabelledIngredients
        public ActionResult Index()
        {
            List<HighRiskLabelledIngredient> highRiskLabelledIngedientList = new List<HighRiskLabelledIngredient>();
            List<int> highRiskList = db.UserIngredients.Where(u => u.Label == "High-Risk").Select(u => u.LabelledIngredientID).Distinct().ToList();
            foreach (int ingredient in highRiskList)
            {
                LabelledIngredient labelIng = db.LabelledIngredients.Where(l => l.ID == ingredient).FirstOrDefault();
                int count = db.UserIngredients.Where(u => u.LabelledIngredientID == ingredient && u.Label == "High-Risk").Count();
                HighRiskLabelledIngredient newIngredient = new HighRiskLabelledIngredient(labelIng, count);
                highRiskLabelledIngedientList.Add(newIngredient);
            }
            highRiskLabelledIngedientList = highRiskLabelledIngedientList.OrderByDescending(o => o.countOfLabelOccurences).ToList();
            return View(highRiskLabelledIngedientList);
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
