using System;
using MongoDB.Bson;

public class MongoDBKeyGenerator
{
    public static string GenerateMongoDBKey()
    {
        ObjectId objectId = ObjectId.GenerateNewId();
        return objectId.ToString();
    }

}
