using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BasicApiController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
