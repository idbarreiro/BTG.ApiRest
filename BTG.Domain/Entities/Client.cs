using MongoDB.Bson.Serialization;

namespace BTG.Domain.Entities
{
    public class Client
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Estimate { get; set; }
        public string TypeNotification { get; set; }
    }
}
