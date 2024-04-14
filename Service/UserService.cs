using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _users = database.GetCollection<User>("Users");
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _users.Find(user => true).ToListAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateUserAsync(Guid id, User updatedUser)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Id, id);
        var update = Builders<User>.Update
            .Set(user => user.Name, updatedUser.Name)
            .Set(user => user.Email, updatedUser.Email);
        // Add other fields as necessary

        await _users.UpdateOneAsync(filter, update);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _users.DeleteOneAsync(user => user.Id == id);
    }
}
