using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("priority")]
        public int Priority { get; set; }

        [BsonElement("ProjectId")]
        public string ProjectId { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; } 

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("progress")]
        public int Progress { get; set; }
    }
