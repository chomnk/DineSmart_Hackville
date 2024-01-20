using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DineSmartWebAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string UserName { get; set; } = "default";

        public Dictionary<string, KeyValuePair<string, string>> ListOfReviews { get; set; } = new();

        public string Password { get; set; } = "1235456";

        public string? WaitListId { get; set; } = null;

    }
}
