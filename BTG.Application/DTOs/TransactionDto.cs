using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Application.DTOs
{
    public class TransactionDto
    {
        public string Id { get; set; }
        public int FundId { get; set; }
        public string FundName { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
