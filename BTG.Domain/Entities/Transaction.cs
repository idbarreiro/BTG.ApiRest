using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BTG.Domain.Entities
{
    public class Transaction
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int FundId { get; set; }
        public string FundName { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
    }
}
