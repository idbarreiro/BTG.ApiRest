using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BTG.Domain.Entities
{
    public class Client
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Estimate { get; set; }
        public int TypeNotification { get; set; }
    }
}
