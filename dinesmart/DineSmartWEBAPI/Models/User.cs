using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DineSmartWEBAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        //I can replace this with Guid later. Maybe now?
        //public string? Id {get ; set;}

        public string Id { get; set; }

        [BsonElement("Name")]

        public string Username {get; set; } = null!;

        //password???

        //public string Password {get; set;}

        public List<string> ListOfReviews {get; set; } = new();

        public string? RestaurantId {get; set; } = null;
    }
}