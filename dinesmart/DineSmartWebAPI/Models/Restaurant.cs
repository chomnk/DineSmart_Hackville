using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DineSmartWebAPI.Models
{
    public class Restaurant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("Name")]
        public string? RestaurantName { get; set; } = "Restaurant Name";
        public string? ImageLink { get; set; } = "https://www.silvea-architecte.fr/wp-content/uploads/2019/02/LINK-BANGKOK-12-1.jpg";
        public int AverageCost { get; set; } = 3;
        public int PeopleInQueue { get; set; } = 0;
        
    }
}
