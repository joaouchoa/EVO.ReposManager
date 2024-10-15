using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Application.DTOs;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetFavoriteRepos;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories;
using EVO.ReposManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public class GetRepoByNameQueryHandler : IRequestHandler<GetRepoByNameQuery, GetRepoByNameQueryResponse>
    {
        private readonly GetRepoByNameQueryValidation _validator;
        private readonly IReposReadRepository _repository;

        public GetRepoByNameQueryHandler(IReposReadRepository repository, GetRepoByNameQueryValidation validator)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<GetRepoByNameQueryResponse> Handle(GetRepoByNameQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GetRepoByNameQueryResponse(false, default, default, validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            var totalCount = await _repository.GetByRepositoryByNameCountAsync(request.repoName);

            if (totalCount == 0)
                return default;

            int totalPages = (int)Math.Ceiling((double)totalCount / request.perPage);

            var page = request.page;
            if (request.page > totalPages && totalPages > 0)
                page = totalPages;

            var response = await _repository.GetByRepositoryByNameAsync(request.repoName, page, request.perPage);

            if (response is null || response.Repositories.Count == 0)
                return default;

            var repositories = response.Repositories.Select(repo => new Repo
            {
                Id = repo.Id,
                Name = repo.Name,
                Description = repo.Description,
                Url = repo.Url,
                Language = repo.Language,
                Owner = repo.Owner.Login
            }).ToList();

            // Verifica se há mais páginas
            bool hasMorePages = page < totalPages;


            var SucessResponse = new GetRepoByNameQueryResponse(hasMorePages, totalPages, repositories, default);
            return SucessResponse;
        }
    }
}
