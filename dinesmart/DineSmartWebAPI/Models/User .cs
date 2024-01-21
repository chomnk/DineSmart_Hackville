using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DineSmartWebAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string userName { get; set; } = "default";

        public Dictionary<string, KeyValuePair<string, string>> listOfReviews { get; set; } = new();

        public string password { get; set; } = "1235456";

        public string? waitListId { get; set; } = null;

    }
}
