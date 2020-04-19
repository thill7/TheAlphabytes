using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using FODfinder.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace FODfinder.Controllers
{
    [Authorize]
    public class SavedFoodsController : Controller
    {
        private FFDBContext db = new FFDBContext();

        public int countSavedFoods(List<SavedFood> savedFoodsList)
        {
            int count = savedFoodsList.Count();
            return count;
        }

        // GET: SavedFoods
        public ActionResult Index(int? id)
        {
            var uid = User.Identity.GetUserId();
            try
            {
                UserList userList = db.UserLists.FirstOrDefault(x => x.listID == id);
                if (userList.userID != uid)
                {
                    return RedirectToAction("Index", "UserLists");
                }
                ViewBag.ListName = userList.listName;
            }
            catch
            {
                return RedirectToAction("Index", "UserLists");
            }
            List<SavedFood> savedFoodsList = db.SavedFoods.Where(sf => sf.listID == id).ToList();
            ViewBag.TotalSavedFoods = countSavedFoods(savedFoodsList);
            return View(savedFoodsList);
        }

        // GET: SavedFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedFood savedFood = db.SavedFoods.Find(id);
            if (savedFood == null)
            {
                return HttpNotFound();
            }
            return View(savedFood);
        }

        // POST: SavedFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ContentResult Create(int usdaFoodID, int listID, string brandOwner, string upc, string description)
        {
            if (User.Identity.GetUserId() != null)
            {
                SavedFood savedFood = new SavedFood(usdaFoodID, listID, brandOwner, upc, description);
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

        // GET: SavedFoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedFood savedFood = db.SavedFoods.Find(id);
            if (savedFood == null)
            {
                return HttpNotFound();
            }
            return View(savedFood);
        }

        // POST: SavedFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usdaFoodID,userID")] SavedFood savedFood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savedFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(savedFood);
        }

        // GET: SavedFoods/Delete/5
        public ActionResult Delete(int? usdaFoodID, int? listID)
        {
            if (usdaFoodID == null || listID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedFood savedFood = db.SavedFoods.Find(usdaFoodID, listID);
            if (savedFood == null)
            {
                return HttpNotFound();
            }
            return View(savedFood);
        }

        // POST: SavedFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int usdaFoodID, int listID)
        {
            SavedFood savedFood = db.SavedFoods.Find(usdaFoodID, listID);
            db.SavedFoods.Remove(savedFood);
            db.SaveChanges();
            return RedirectToAction("Index/" + listID);
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
