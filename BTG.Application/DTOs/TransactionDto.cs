using BTG.Domain.Entities;

namespace BTG.Application.DTOs
{
    public class TransactionDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public FundDto Fund { get; set; }
    }
}
