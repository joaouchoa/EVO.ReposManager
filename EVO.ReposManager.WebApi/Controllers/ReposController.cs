using EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EVO.ReposManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ApiController
    {
        private readonly IMediator _mediator;

        public ReposController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]/{fullName}", Name = "GetRepositories")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDirector([FromRoute] string fullName)
        {
            var query = new GetReposQuery(fullName);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }
    }
}
