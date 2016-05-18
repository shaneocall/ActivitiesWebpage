using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActivitiesWebpage.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ActivitiesWebpage.Controllers
{
    public class HomeController : Controller
    {
        private IMongoCollection<Activity> _repository;

        // GET: Home
        public ActionResult Index()
        {
            ConnectToDb();

            return View();
        }


        private void ConnectToDb()
        {
            string connectionString = ConfigurationManager.AppSettings["ActivitiesDb"];

            var mongoClient = new MongoClient(connectionString);

            var mongoDb = mongoClient.GetDatabase("Activities");

            IMongoCollection<Activity> _repository = mongoDb.GetCollection<Activity>(typeof(Activity).Name);
            var timesheets = new List<Activity>
            {
                new Activity { Id = "12345", Name = "Football", DateStarted = new DateTime(2011, 7, 2), DateEnded = new DateTime(2015, 7, 2), GamesPlayed = 10},
                new Activity { Id = "678910", Name = "Rugby", DateStarted = new DateTime(2011, 7, 2), DateEnded = new DateTime(2015, 7, 2), GamesPlayed = 25},
                new Activity { Id = "11121314", Name = "Hurling", DateStarted = new DateTime(2011, 7, 2), DateEnded = new DateTime(2015, 7, 2), GamesPlayed = 9},
                new Activity { Id = "151617", Name = "Swimming", DateStarted = new DateTime(2011, 7, 2), DateEnded = new DateTime(2015, 7, 2), GamesPlayed = 13},
            };

            _repository.DeleteMany(new ExpressionFilterDefinition<Activity>(activity => activity.Id != null));

            foreach (var timesheet in timesheets)
                _repository.InsertOne(timesheet);

        }

        public JsonResult GetJson()
        {
            var jsonResult = new JsonResult { Data = _repository.Database.ToJson() };

            return jsonResult;
        }

        //
        // GET: /Activities/Details/5

        public ActionResult Details(int id = 0)
        {
           // Activity activity = db.MyTecBitsDB.Find(id);
            var activity =
                _repository.FindSync(new ExpressionFilterDefinition<Activity>(x => x.Id == id.ToString()))
                    .FirstOrDefault();


            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // GET: /Activities/Create

        public ActionResult Create()
        {
            return View();
        }

     
        //
        // POST: /Activities/Create

        [HttpPost]
        public String Create(Activity activity)
        {

            _repository.InsertOne(activity);
            return "success";
        }

        //
        // GET: /Activities/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var activity =
                _repository.FindSync(new ExpressionFilterDefinition<Activity>(x => x.Id == id.ToString()))
                    .FirstOrDefault();

            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // POST: /Activities/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Activity activity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(activity).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(activity);
        //}

        //
        // GET: /Activities/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var activity =
                 _repository.FindSync(new ExpressionFilterDefinition<Activity>(x => x.Id == id.ToString()))
                     .FirstOrDefault();

            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // POST: /Activities/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            _repository.DeleteOne(new ExpressionFilterDefinition<Activity>(x => x.Id == id.ToString()));
            return RedirectToAction("Index");
        }
    }
}