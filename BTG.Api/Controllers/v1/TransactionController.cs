using Asp.Versioning;
using BTG.Application.Features.Transactions.Commands.CreateTransactionCommand;
using BTG.Application.Features.Transactions.Queries.GetLatestTransactions;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TransactionController : BasicApiController
    {
        //Post api/v1/transactions/fund-suscribe
        [HttpPost]
        public async Task<IActionResult> FundSuscribe([FromBody] CreateTransactionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //Get api/v1/transactions/GetLatestTransactions
        [HttpGet]
        public async Task<IActionResult> GetLatestTransactions()
        {
            return Ok(await Mediator.Send(new GetLatestTransactionsQuery()));
        }
    }
}
