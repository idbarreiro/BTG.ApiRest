using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace BTG.Domain.Entities
{
    public class Fund
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int FundId { get; set; }
        public string Name { get; set; }
        public decimal MinAmount { get; set; }
        public string Category { get; set; }
    }
}
