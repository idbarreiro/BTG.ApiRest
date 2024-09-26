using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BTG.Domain.Entities
{
    public class Notification
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Means { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}
