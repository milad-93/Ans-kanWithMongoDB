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
    [BsonIgnoreExtraElements] // if not it crash it skips  to deserialize the ignored one
    public class Apply
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [DisplayName("[Application-ID]")]
        public ObjectId _id { get; set; }

        [BsonIgnore]
        public string _idString
        {
            get
            {
                return _id.ToString();
            }
        }


        [Required]
        [DisplayName("Name")]
        public string name { get; set; }
        [Required]
        [DisplayName("Lastname")]
        public string lastname { get; set; }

        [Required]
        [DisplayName("Phonenumber")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Email")]
        public string email { get; set; }
        [Required]
        [DisplayName("Salary")]
        public string Salary { get; set; }
        [Required]
        [DisplayName("Social security number")]
        public string socSecNum { get; set; }
        [Required]
        [DisplayName("[Apartment-ID]")]
        public string ApartmentId { get; set; }

        // converting the id to a string in order to show it in the userinterface


    }
}