using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ansökan.Models
{
    //https://stackoverflow.com/questions/11651254/how-to-change-the-display-name-for-labelfor-in-razor-in-mvc3
    //use this to specify in view instead of class but remove displayName over the attribute in order to work.
    //  @Html.LabelFor(model => model.adress, "ADress"
  
    public class Apartment
    {
       public static IMongoCollection<Apartment> apartmentCollection { get; set; }


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [DisplayName("-[Post Id]-")]
        public ObjectId _id { get; set; }

        [Required]
        [DisplayName("Adress")]
        public string adress { get; set; }
        
        [Required]
        [DisplayName("Elevator in the building?")]      
        public string elevator { get; set; }
       
        [Required]
        [DisplayName("How many roooms?")]              
        public string nOfRooms { get; set; }
        [Required]
        [DisplayName("Does the apartment have a balcony?")]    
        public string balcony { get; set; }
        [Required]
        [DisplayName ("Squaremeter")]
        public string squareMeter { get; set; }
        [Required]
        [DisplayName ("Floor of apartment")]
        public string floor { get; set; }
    }

    
}