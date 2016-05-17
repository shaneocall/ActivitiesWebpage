using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActivitiesWebpage.Models;
using MongoDB.Driver;

namespace ActivitiesWebpage.Controllers
{
    public class HomeController : Controller
    {
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

            var repository = mongoDb.GetCollection<Activity>(typeof(Activity).Name);
            var timesheets = new List<Activity>
            {
                new Activity { Id = "12345", Name = "Football", DateStarted = new DateTime(2011, 7, 2), DateEnded = new DateTime(2015, 7, 2), GamesPlayed = 10},
                new Activity { Id = "678910", Name = "Rugby", DateStarted = new DateTime(2011, 7, 2), DateEnded = new DateTime(2015, 7, 2), GamesPlayed = 25},
                new Activity { Id = "11121314", Name = "Hurling", DateStarted = new DateTime(2011, 7, 2), DateEnded = new DateTime(2015, 7, 2), GamesPlayed = 9},
                new Activity { Id = "151617", Name = "Swimming", DateStarted = new DateTime(2011, 7, 2), DateEnded = new DateTime(2015, 7, 2), GamesPlayed = 13},
            };

            repository.DeleteMany(new ExpressionFilterDefinition<Activity>(activity => activity.Id != null));

            foreach (var timesheet in timesheets)
                repository.InsertOne(timesheet);

        }
    }
}