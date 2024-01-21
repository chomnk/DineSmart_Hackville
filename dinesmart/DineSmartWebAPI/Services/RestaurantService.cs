using DineSmartWebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DineSmartWebAPI.Services;

public class RestaurantService
{
    private readonly IMongoCollection<Restaurant> _restaurantCollection;
    private readonly IMongoCollection<User> _userCollection;

    public RestaurantService(
        IOptions<RestaurantDatabaseSettings> RestaurantDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            RestaurantDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            RestaurantDatabaseSettings.Value.DatabaseName);

        _restaurantCollection = mongoDatabase.GetCollection<Restaurant>(
            RestaurantDatabaseSettings.Value.RestaurantsCollectionName);

        _userCollection = mongoDatabase.GetCollection<User>(
            RestaurantDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<Restaurant>> GetRestaurantAsync() =>
        await _restaurantCollection.Find(_ => true).ToListAsync();

    public async Task<Restaurant> GetSpecificRestaurantAsync(string restaurantName) =>
        await _restaurantCollection.Find(x => x.RestaurantName == restaurantName).FirstOrDefaultAsync();

    public async Task CreateRestaurantAsync(Restaurant newRestaurant)
    {
        await _restaurantCollection.InsertOneAsync(newRestaurant);
    }

    public async Task<List<User>> GetUserAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string userName) =>
        await _userCollection.Find(x => x.UserName == userName).FirstOrDefaultAsync();

    public async Task<List<User>> GetTime(string id) =>
        await _userCollection.Find(x => x.WaitListId == id).ToListAsync();
    //await _userCollection.Find(x => x.ListOfReviews. == id).ToListAsync();

    public async Task CreateUserAsync(User newUser)
    {
        newUser.Id = ObjectId.GenerateNewId();
        await _userCollection.InsertOneAsync(newUser);
    }

    public async Task UpdateUserAsync(string userName, User updatedUser) =>
        await _userCollection.ReplaceOneAsync(x => x.UserName == userName, updatedUser);

    public async Task UpdateRestaurantAsync(string userName, Restaurant updatedRestaurant) =>
        await _restaurantCollection.ReplaceOneAsync(x => x.RestaurantName == userName, updatedRestaurant);

    public async Task RemoveAsync(string userName) =>
        await _userCollection.DeleteOneAsync(x => x.UserName == userName);

}