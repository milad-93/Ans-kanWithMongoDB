using Ansökan.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ansökan.Controllers
{
    public class UserApplyController : Controller
    {
        const string ConnectionClient = "mongodb+srv://oggy:oggy@cluster0.sxkm8.gcp.mongodb.net/<dbname>?retryWrites=true&w=majority";
        const string DataBaseName = "apartments_db";
        const string CollectionName = "ApartmentApplications"; // used for the database name  
     
        public ActionResult Index()
        {
            try
            {
                var client = new MongoClient(ConnectionClient);
                var database = client.GetDatabase(DataBaseName);
                //Will convert my Apartment collection of BSON to list of object of Apartment
                List<Apply> ListOfApplications = database.GetCollection<Apply>(CollectionName).AsQueryable().ToList();
                return View(ListOfApplications);

            }
            catch (Exception)
            {

                throw;
            }
           
        }

       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Apply application)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new MongoClient(ConnectionClient);
                    var database = client.GetDatabase(DataBaseName);
                    var documentCollection = database.GetCollection<BsonDocument>(CollectionName);

                    //uniqe id
                    application._id = ObjectId.GenerateNewId();

                    string apartmentJSON = Newtonsoft.Json.JsonConvert.SerializeObject(application);
                    MongoDB.Bson.BsonDocument newApartmentDocument = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(apartmentJSON);

                    documentCollection.InsertOneAsync(newApartmentDocument);
                    return RedirectToAction("Index", "UserApply");

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    

 

     
    }
}
