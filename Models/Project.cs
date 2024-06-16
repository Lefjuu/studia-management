using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoAuthenticatorAPI.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
