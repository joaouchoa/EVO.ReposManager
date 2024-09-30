using EVO.ReposManager.Application.Features.Repositories.Commands.CreateFavoriteRepo;
using EVO.ReposManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo;
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

        [HttpGet("[action]/{userName}", Name = "GetRepositoriesByOnwer")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetRepositoriesByUserName([FromRoute] string userName)
        {
            var query = new GetReposByOwnerQuery(userName);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            if(response.Repositories is null && response.Errors.Count > 0)
                return CustomResponse((int)HttpStatusCode.BadRequest, false, response);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpDelete("[action]/{id}", Name = "DeleteRepositoryById")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteRepositoryById([FromRoute] long id)
        {
            var query = new DeleteFavoriteRepoCommand(id);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            if (!response.created && response.Errors.Count > 0)
                return CustomResponse((int)HttpStatusCode.BadRequest, response.created, response);

            return CustomResponse((int)HttpStatusCode.Created, response.created, response);
        }

        [HttpPost("CreateFavoriteRepository")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> CreateFavoriteRepository([FromBody] CreateFavoriteRepoCommand repository)
        {
            var query = new CreateFavoriteRepoCommand(repository.Id, repository.Name, repository.Description,repository.Url, repository.Language);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            if (!response.created && response.Errors.Count > 0)
                return CustomResponse((int)HttpStatusCode.BadRequest, response.created, response);

            return CustomResponse((int)HttpStatusCode.Created, response.created, response);
        }
    }
}
