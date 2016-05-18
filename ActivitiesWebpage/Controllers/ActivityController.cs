using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ActivitiesWebpage.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace ActivitiesWebpage.Controllers
{
    public class ActivityController : ApiController
    {
        private readonly MongoDatabase _mongoDb;
        private readonly MongoCollection<Activity> _repository;

        public ActivityController()
        {
            // var connectionString = ConfigurationManager.AppSettings["MongoDBactivities"];
            //_mongoDb = MongoDatabase.Create(connectionString);

            string connectionString = ConfigurationManager.AppSettings["ActivitiesDb"];

            var mongoClient = new MongoClient(connectionString);
            var server = mongoClient.GetServer();

            _mongoDb = server.GetDatabase("ActivitiesDb");

            _repository = _mongoDb.GetCollection<Activity>(typeof(Activity).Name);
        }

        public JsonResult GetJson()
        {
            var jsonResult = new JsonResult {Data = _repository.Database.ToJson()};
            
            return jsonResult;
        }

        // GET /api/activities
        public HttpResponseMessage Get()
        {
            var activities = _repository.FindAll().ToList();
            var response = Request.CreateResponse(HttpStatusCode.OK, activities);
            string uri = Url.Route(null, null);
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        // GET /api/activities/4fd63a86f65e0a0e84e510de
        [System.Web.Http.HttpGet]
        public Activity Get(string id)
        {
            var query = Query.EQ("_id", new ObjectId(id));
            return _repository.Find(query).Single();
        }

        // POST /api/activities
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post(Activity activity)
        {
            _repository.Insert(activity);
            string uri = Url.Route(null, new { id = activity.Id }); 
            var response = Request.CreateResponse(HttpStatusCode.Created, activity);
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        // PUT /api/activities
        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put(Activity activity)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, activity);
            _repository.Save(activity);
            string uri = Url.Route(null, new { id = activity.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        // DELETE /api/activities/4fd63a86f65e0a0e84e510de
        public HttpResponseMessage Delete(params string[] ids)
        {
            foreach (var id in ids)
            {
                _repository.Remove(Query.EQ("_id", new ObjectId(id)));
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
