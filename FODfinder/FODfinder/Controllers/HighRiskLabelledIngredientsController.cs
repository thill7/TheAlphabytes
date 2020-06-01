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

        public List<HighRiskLabelledIngredient> RetrieveRiskyIngredientsAge1()
        {
            using (FFDBContext db = new FFDBContext())
            {
                List<HighRiskLabelledIngredient> highRiskLabelledIngredientList = new List<HighRiskLabelledIngredient>();
                List<int> highRiskList = db.UserIngredients.Where(u => u.Label == "High-Risk").Select(u => u.LabelledIngredientID).Distinct().ToList();
                foreach (int ingredient in highRiskList)
                {
                    LabelledIngredient labelIng = db.LabelledIngredients.Where(l => l.ID == ingredient).FirstOrDefault();
                    int count = db.UserIngredients.Where(u => u.LabelledIngredientID == ingredient && u.Label == "High-Risk" && (DateTime.Now.Year - u.AspNetUser.UserInformation.birthdate.Year) < 18).Count();
                    HighRiskLabelledIngredient newIngredient = new HighRiskLabelledIngredient(labelIng, count);
                    highRiskLabelledIngredientList.Add(newIngredient);
                }
                highRiskLabelledIngredientList = highRiskLabelledIngredientList.OrderByDescending(o => o.countOfLabelOccurences).ToList();
                return highRiskLabelledIngredientList;
            }
        }

        public List<HighRiskLabelledIngredient> RetrieveRiskyIngredientsAge2()
        {
            using (FFDBContext db = new FFDBContext())
            {
                List<HighRiskLabelledIngredient> highRiskLabelledIngredientList = new List<HighRiskLabelledIngredient>();
                List<int> highRiskList = db.UserIngredients.Where(u => u.Label == "High-Risk").Select(u => u.LabelledIngredientID).Distinct().ToList();
                foreach (int ingredient in highRiskList)
                {
                    LabelledIngredient labelIng = db.LabelledIngredients.Where(l => l.ID == ingredient).FirstOrDefault();
                    int count = db.UserIngredients.Where(u => u.LabelledIngredientID == ingredient && u.Label == "High-Risk" && (DateTime.Now.Year - u.AspNetUser.UserInformation.birthdate.Year) > 17 && (DateTime.Now.Year - u.AspNetUser.UserInformation.birthdate.Year) < 36).Count();
                    HighRiskLabelledIngredient newIngredient = new HighRiskLabelledIngredient(labelIng, count);
                    highRiskLabelledIngredientList.Add(newIngredient);
                }
                highRiskLabelledIngredientList = highRiskLabelledIngredientList.OrderByDescending(o => o.countOfLabelOccurences).ToList();
                return highRiskLabelledIngredientList;
            }
        }

        public List<HighRiskLabelledIngredient> RetrieveRiskyIngredientsAge3()
        {
            using (FFDBContext db = new FFDBContext())
            {
                List<HighRiskLabelledIngredient> highRiskLabelledIngredientList = new List<HighRiskLabelledIngredient>();
                List<int> highRiskList = db.UserIngredients.Where(u => u.Label == "High-Risk").Select(u => u.LabelledIngredientID).Distinct().ToList();
                foreach (int ingredient in highRiskList)
                {
                    LabelledIngredient labelIng = db.LabelledIngredients.Where(l => l.ID == ingredient).FirstOrDefault();
                    int count = db.UserIngredients.Where(u => u.LabelledIngredientID == ingredient && u.Label == "High-Risk" && (DateTime.Now.Year - u.AspNetUser.UserInformation.birthdate.Year) > 35 && (DateTime.Now.Year - u.AspNetUser.UserInformation.birthdate.Year) < 56).Count();
                    HighRiskLabelledIngredient newIngredient = new HighRiskLabelledIngredient(labelIng, count);
                    highRiskLabelledIngredientList.Add(newIngredient);
                }
                highRiskLabelledIngredientList = highRiskLabelledIngredientList.OrderByDescending(o => o.countOfLabelOccurences).ToList();
                return highRiskLabelledIngredientList;
            }
        }

        public List<HighRiskLabelledIngredient> RetrieveRiskyIngredientsAge4()
        {
            using (FFDBContext db = new FFDBContext())
            {
                List<HighRiskLabelledIngredient> highRiskLabelledIngredientList = new List<HighRiskLabelledIngredient>();
                List<int> highRiskList = db.UserIngredients.Where(u => u.Label == "High-Risk").Select(u => u.LabelledIngredientID).Distinct().ToList();
                foreach (int ingredient in highRiskList)
                {
                    LabelledIngredient labelIng = db.LabelledIngredients.Where(l => l.ID == ingredient).FirstOrDefault();
                    int count = db.UserIngredients.Where(u => u.LabelledIngredientID == ingredient && u.Label == "High-Risk" && (DateTime.Now.Year - u.AspNetUser.UserInformation.birthdate.Year) > 55 && (DateTime.Now.Year - u.AspNetUser.UserInformation.birthdate.Year) < 76).Count();
                    HighRiskLabelledIngredient newIngredient = new HighRiskLabelledIngredient(labelIng, count);
                    highRiskLabelledIngredientList.Add(newIngredient);
                }
                highRiskLabelledIngredientList = highRiskLabelledIngredientList.OrderByDescending(o => o.countOfLabelOccurences).ToList();
                return highRiskLabelledIngredientList;
            }
        }

        public List<HighRiskLabelledIngredient> RetrieveRiskyIngredientsAge5()
        {
            using (FFDBContext db = new FFDBContext())
            {
                List<HighRiskLabelledIngredient> highRiskLabelledIngredientList = new List<HighRiskLabelledIngredient>();
                List<int> highRiskList = db.UserIngredients.Where(u => u.Label == "High-Risk").Select(u => u.LabelledIngredientID).Distinct().ToList();
                foreach (int ingredient in highRiskList)
                {
                    LabelledIngredient labelIng = db.LabelledIngredients.Where(l => l.ID == ingredient).FirstOrDefault();
                    int count = db.UserIngredients.Where(u => u.LabelledIngredientID == ingredient && u.Label == "High-Risk" && (DateTime.Now.Year - u.AspNetUser.UserInformation.birthdate.Year) > 75).Count();
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

        public JsonResult GetAgeChart1()
        {
            var highRiskLabelledIngredientsAge1 = RetrieveRiskyIngredientsAge1();
            return Json(highRiskLabelledIngredientsAge1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAgeChart2()
        {
            var highRiskLabelledIngredientsAge2 = RetrieveRiskyIngredientsAge2();
            return Json(highRiskLabelledIngredientsAge2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgeChart3()
        {
            var highRiskLabelledIngredientsAge3 = RetrieveRiskyIngredientsAge3();
            return Json(highRiskLabelledIngredientsAge3, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgeChart4()
        {
            var highRiskLabelledIngredientsAge4 = RetrieveRiskyIngredientsAge4();
            return Json(highRiskLabelledIngredientsAge4, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgeChart5()
        {
            var highRiskLabelledIngredientsAge5 = RetrieveRiskyIngredientsAge5();
            return Json(highRiskLabelledIngredientsAge5, JsonRequestBehavior.AllowGet);
        }
    }
}
