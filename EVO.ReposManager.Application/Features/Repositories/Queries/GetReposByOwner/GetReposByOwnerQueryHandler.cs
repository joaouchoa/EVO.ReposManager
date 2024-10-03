using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories
{
    public class GetReposByOwnerQueryHandler : IRequestHandler<GetReposByOwnerQuery, GetReposByOwnerQueryResponse>
    {
        private readonly IReposReadRepository _repository;
        private readonly GetReposByOwnerQueryValidation _validator;

        public GetReposByOwnerQueryHandler(IReposReadRepository repository, GetReposByOwnerQueryValidation validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<GetReposByOwnerQueryResponse> Handle(GetReposByOwnerQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GetReposByOwnerQueryResponse(default, validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            var repos = await _repository.GetRepositoriesByUserAsync(request.UserName);

            if (repos is null || repos.Count == 0)
                return default;

            // Converter de GitHubResponse para Repo
            var repositories = repos?.Select(gitHubRepo => new Repo
            {
                Id = gitHubRepo.Id,
                Name = gitHubRepo.Name,
                Description = gitHubRepo.Description,
                Url = gitHubRepo.Url,
                Language = gitHubRepo.Language,
                Owner = gitHubRepo.Owner.Login
            }).ToList() ?? new List<Repo>();

            var Sucessresponse = new GetReposByOwnerQueryResponse(repositories,default);
            return Sucessresponse;
        }
    }
}
