// using DineSmartWEBAPI.Models;
// using Microsoft.Extensions.Options;
// using MongoDB.Driver;

// namespace BookStoreApi.Services;

// public class RestaurantService
// {
//     private readonly IMongoCollection<Restaurant> _restaurantCollection;
//     private readonly IMongoCollection<User> _userCollection;
//     public RestaurantService(
//         IOptions<RestaurantDatabase> restaurantDatabaseSettings)
//     {
//         var mongoClient = new MongoClient(
//             restaurantDatabaseSettings.Value.ConnectionString);

//         var mongoDatabase = mongoClient.GetDatabase(
//             restaurantDatabaseSettings.Value.DatabaseName);

//         _restaurantCollection = mongoDatabase.GetCollection<Restaurant>(
//             restaurantDatabaseSettings.Value.RestaurantCollectionName);
//     }



//     //The logic here is not pertaining to the logic we discussed. I'm following https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-8.0&tabs=visual-studio-code
//     //in order to get the project working. I'll fix the logic later when the project runs smoothly!
    
//     public async Task<List<Restaurant>> GetAsync() =>
//         await _restaurantCollection.Find(_ => true).ToListAsync();

//     public async Task<Restaurant?> GetAsync(string id) =>
//         await _restaurantCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

//     public async Task CreateAsync(Restaurant newRestaurant) =>
//         await _restaurantCollection.InsertOneAsync(newRestaurant);

//     public async Task UpdateAsync(string id, Restaurant updatedRestaurant) =>
//         await _restaurantCollection.ReplaceOneAsync(x => x.Id == id, updatedRestaurant);

//     public async Task RemoveAsync(string id) =>
//         await _restaurantCollection.DeleteOneAsync(x => x.Id == id);
// }