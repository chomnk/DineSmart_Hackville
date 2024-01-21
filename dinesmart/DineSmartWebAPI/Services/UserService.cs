/*
 * 
 * using DineSmartWebAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DineSmartWebAPI.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(
        IOptions<UserDatabaseSettings> UserDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            UserDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            UserDatabaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(
            UserDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string userName) =>
        await _userCollection.Find(x => x.UserName == userName).FirstOrDefaultAsync();

    public async Task<List<User>> GetTime(string id) =>
        await _userCollection.Find(x => x.WaitListId == id).ToListAsync();
    //await _userCollection.Find(x => x.ListOfReviews. == id).ToListAsync();

    public async Task CreateAsync(User newUser)
    {
        newUser.Id = ObjectId.GenerateNewId();
        await _userCollection.InsertOneAsync(newUser);
    }
        

    public async Task UpdateAsync(string userName, User updatedUser) =>
        await _userCollection.ReplaceOneAsync(x => x.UserName == userName, updatedUser);

    public async Task RemoveAsync(string userName) =>
        await _userCollection.DeleteOneAsync(x => x.UserName == userName);
}

*/