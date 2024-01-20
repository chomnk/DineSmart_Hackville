using DineSmartWEBAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;
    public UserService(
        IOptions<UserDatabase> userDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            userDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userDatabaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(
            userDatabaseSettings.Value.UserCollectionName);
    }



    //The logic here is not pertaining to the logic we discussed. I'm following https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-8.0&tabs=visual-studio-code
    //in order to get the project working. I'll fix the logic later when the project runs smoothly!
    
    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newRestaurant) =>
        await _userCollection.InsertOneAsync(newRestaurant);

    public async Task UpdateAsync(string id, User updatedRestaurant) =>
        await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedRestaurant);

    public async Task RemoveAsync(string id) =>
        await _userCollection.DeleteOneAsync(x => x.Id == id);
}