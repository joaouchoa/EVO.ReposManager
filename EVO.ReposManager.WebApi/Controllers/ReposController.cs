using EVO.ReposManager.Application.Features.Repositories.Commands.CreateFavoriteRepo;
using EVO.ReposManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetFavoriteRepos;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetRepoByName;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

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
        public async Task<ActionResult> GetRepositoriesByUserName([FromRoute] string userName, int page, int perPage)
        {
            var query = new GetReposByOwnerQuery(userName, page, perPage);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            if(response.Repositories is null && response.Errors.Count > 0)
                return CustomResponse((int)HttpStatusCode.BadRequest, false, response);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpGet("[action]/{repositoryName}", Name = "GetRepositoriesByName")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetRepositoriesByName([FromRoute] string repositoryName, int page, int perPage)
        {
            var query = new GetRepoByNameQuery(repositoryName, page, perPage);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            if (response.Repositories is null && response.Errors.Count > 0)
                return CustomResponse((int)HttpStatusCode.BadRequest, false, response);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpGet("GetFavoriteRepositories")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetFavoriteRepositories(int page, int perPage)
        {
            var query = new GetFavoriteReposQuery(page, perPage);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpDelete("[action]/{id}", Name = "DeleteFavoriteRepositoryById")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteFavoriteRepositoryById([FromRoute] long id)
        {
            var query = new DeleteFavoriteRepoCommand(id);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            if (!response.created && response.Errors.Count > 0)
                return CustomResponse((int)HttpStatusCode.BadRequest, response.created, response);

            return CustomResponse((int)HttpStatusCode.NoContent, response.created, default);
        }

        [HttpPost("CreateFavoriteRepository")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> CreateFavoriteRepository([FromBody] CreateFavoriteRepoCommand repository)
        {
            var query = new CreateFavoriteRepoCommand(repository.Id, repository.Name, repository.Description,repository.Url, repository.Language, repository.Owner);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            if (!response.created && response.Errors.Count > 0)
                return CustomResponse((int)HttpStatusCode.BadRequest, response.created, response.Errors);

            return CustomResponse((int)HttpStatusCode.Created, response.created, default);
        }

    }
}
