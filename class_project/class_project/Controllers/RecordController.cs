using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using class_project.DAL;
using Newtonsoft.Json.Linq;
using class_project.Models;

namespace class_project.Controllers
{
    public class RecordController : Controller
    {
        private CPDBContext cpdb = new CPDBContext();
        // GET: Record

        public ContentResult RecordAthletes()
        {
            JArray jsonData = JArray.FromObject(cpdb.Records.ToList().GroupBy(r => r.AthleteID).Select(r => new { r.FirstOrDefault().AthleteID, Name = (r.LastOrDefault().Athlete.Person.FirstName + " " + r.LastOrDefault().Athlete.Person.LastName) }));
            return Content(jsonData.ToString(), "application/json");
        }
        [Authorize]
        public ActionResult Detail(int ? Id, int ? eventID, int ? meetID)
        {
            var records = cpdb.Records.ToList()
                .Where(r => cpdb.Athletes.Any(a => a.PersonID == Id))
                .Where(r => Id != null ? r.AthleteID == cpdb.Athletes.Where(a => a.PersonID == Id).FirstOrDefault().ID : true)
                .Where(r => eventID != null ? r.EventID == eventID : true)
                .Where(r => meetID != null ? r.MeetID == meetID : true);

            ViewBag.Return = eventID != null || meetID != null;
            return View(records);
        }

        [Authorize]
        public ActionResult PersonalRecord(int Id)
        {
            var records = cpdb.Records.ToList()
                .Where(r => r.AthleteID == Id)
                .OrderBy(r => r.Time)
                .GroupBy(r => r.EventID)
                .Select(r => r.FirstOrDefault());

            return View(records);
        }
    }
}