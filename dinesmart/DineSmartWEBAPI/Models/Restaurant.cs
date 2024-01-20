// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.Json.Serialization;
// using System.Threading.Tasks;
// using MongoDB.Bson;
// using MongoDB.Bson.Serialization.Attributes;

// namespace DineSmartWEBAPI.Models
// {
//     public class Restaurant
//     {
//         [BsonId]
//         [BsonRepresentation(BsonType.ObjectId)]
//         public string? Id { get; set; }

//         [BsonElement("Name")]
//         [JsonPropertyName("Name")]
//         public string RestaurantName { get; set; } = null!;

//         public decimal Rating { get; set; } //This is the restaurant rating

//         //public string Category { get; set; } = null!;

//         //public string Author { get; set; } = null!;
//     }
// }