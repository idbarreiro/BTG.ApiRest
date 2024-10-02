using Asp.Versioning;
using BTG.Application.Features.Transactions.Queries.GetAllFunds;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FundController : BasicApiController
    {
        //Get api/v1/funds/GetAllFunds
        [HttpGet]
        public async Task<IActionResult> GetAllFunds()
        {
            return Ok(await Mediator.Send(new GetAllFundsQuery()));
        }
    }
}
