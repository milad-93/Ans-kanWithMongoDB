using Ansökan.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Web;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using System;

namespace Ansökan.Controllers
{
  
    public class ApartmentController : Controller
    {
        //Connection information
        const string ConnectionClient = "mongodb+srv://oggy:oggy@cluster0.sxkm8.gcp.mongodb.net/<dbname>?retryWrites=true&w=majority";
        const string DataBaseName = "apartments_db";
        const string CollectionName = "Apartments"; // used for the database name         

        // GET: Apartment
        public ActionResult Index()
        {
            try
            {
                var client = new MongoClient(ConnectionClient);
                var database = client.GetDatabase(DataBaseName);
                //Will convert my Apartment collection of BSON to list of object of Apartment
                List<Apartment> ListOfAddedApartments = database.GetCollection<Apartment>(CollectionName).AsQueryable().ToList();
                return View(ListOfAddedApartments);
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

            
        [HttpPost]     /// adding apartment to To MongoDb    
        public ActionResult Create(Apartment apartment)
        {

            if (ModelState.IsValid)
            {
                var client = new MongoClient(ConnectionClient);
                var database = client.GetDatabase(DataBaseName);
                var documentCollection = database.GetCollection<BsonDocument>(CollectionName);
                                  
               apartment._id = ObjectId.GenerateNewId();

               string apartmentJSON = Newtonsoft.Json.JsonConvert.SerializeObject(apartment);
                MongoDB.Bson.BsonDocument newApartmentDocument = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(apartmentJSON);

                documentCollection.InsertOneAsync(newApartmentDocument);            
                return RedirectToAction("Index", "Apartment");
             
            }

            return View();
         
        }
     
        // GET: Apartment/Edit/5
        public ActionResult Edit(string id)
        {
            // mitt sätt fungerar ja avnänder endast imongocllection 
            var client = new MongoClient(ConnectionClient);
            var database = client.GetDatabase(DataBaseName);

            Models.Apartment.apartmentCollection = database.GetCollection<Models.Apartment>("Apartments");
            var filter = Builders<Models.Apartment>.Filter.Eq("_id", id);
            var result = Models.Apartment.apartmentCollection.Find(filter).FirstOrDefault();

            return View(result); 
/*
            Models.MongoDbClass.ConnectToMongoService();
            Models.MongoDbClass.apartments_collection = MongoDbClass.database.GetCollection<Models.Apartment>("Apartments");

            var filter = Builders<Models.Apartment>.Filter.Eq("_id",id);
            var result = Models.MongoDbClass.apartments_collection.Find(filter).FirstOrDefault();

            return View(result); */



        }
        //https://www.mongodb.com/blog/post/quick-start-csharp-and-mongodb--update-operation
        // POST: Apartment/Edit/5
        [HttpPost]
        public ActionResult  Edit(string id, FormCollection apartment) //taking the id as a string
        {
            try
            {   //connection
                var client = new MongoClient(ConnectionClient);
                var database = client.GetDatabase(DataBaseName);

                Models.Apartment.apartmentCollection = database.GetCollection<Models.Apartment>("Apartments");
                var filter = Builders<Models.Apartment>.Filter.Eq("_id", id); //Filter after current id

                var updateCollection = Builders<Apartment>.Update
                    .Set("adress", apartment["adress"])
                    .Set("elevator", apartment["elevator"])
                    .Set("nOfRooms", apartment["nOfRooms"])
                    .Set("balcony", apartment["balcony"])
                    .Set("squareMeter", apartment["squareMeter"])
                    .Set("floor", apartment["floor"]);

                var result = Apartment.apartmentCollection.UpdateOneAsync(filter, updateCollection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Apartment/Delete/5
        public ActionResult Delete(string id) 
        {
            var client = new MongoClient(ConnectionClient);
            var database = client.GetDatabase(DataBaseName);

            Models.Apartment.apartmentCollection = database.GetCollection<Models.Apartment>("Apartments");
            var filter = Builders<Models.Apartment>.Filter.Eq("_id", id);
            var result = Models.Apartment.apartmentCollection.Find(filter).FirstOrDefault();

            return View(result);
        }

        // POST: Apartment/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var client = new MongoClient(ConnectionClient);
                var database = client.GetDatabase(DataBaseName);

                Models.Apartment.apartmentCollection = database.GetCollection<Models.Apartment>("Apartments");
                var filter = Builders<Models.Apartment>.Filter.Eq("_id", id);

                var delete = Apartment.apartmentCollection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
